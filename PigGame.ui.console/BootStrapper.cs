using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PigGame.lib;
using PigGame.lib.Enums;
using PigGame.lib.Factories;
using PigGame.lib.Models;
using PigGame.lib.Validators;
using PigGame.ui.console.Utilities;

namespace PigGame.ui.console
{
    public class BootStrapper
    {
        public IHost Bootstrap()
        {
            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices((context, services) =>
                            {
                                // register models
                                services.AddSingleton<GameSettingsModel>();
                                services.AddSingleton<DiceModel>();
                                services.AddSingleton<DiceRollModel>();
                                services.AddTransient<TurnModel>();
                                services.AddTransient<PlayerModel>();

                                // register factories
                                services.AddSingleton<PlayerFactory>();
                                services.AddSingleton(provider => new TurnModel.DiceRollModelCreator(() => new DiceRollModel()));
                                services.AddSingleton(provider => new PlayerFactory.TurnCreator(() => new TurnModel(provider.GetRequiredService<TurnModel.DiceRollModelCreator>())));

                                // register validators
                                services.AddSingleton<GameSettingsModel.DiceRollValidatorResolver>(provider => key =>
                                {
                                    switch (key)
                                    {
                                        case GameMode.Normal:
                                            return new SingleDiceRollValidator();
                                        case GameMode.TwoDice:
                                            return new TwoDiceRollValidator();
                                        case GameMode.TwoDiceWithDoubles:
                                            return new TwoDiceRollValidator(true);
                                        case GameMode.BigPig:
                                            return new BigPigRollValidator();
                                        case GameMode.BigPigWithDoubles:
                                            return new BigPigRollValidator(true);
                                        default:
                                            throw new ArgumentOutOfRangeException(nameof(key), key, null);
                                    }
                                });
                                
                                // register utils
                                services.AddSingleton<IDiceRollValidator, SingleDiceRollValidator>();
                                services.AddSingleton<GameEngine>();
                                services.AddSingleton<ConsoleGameService>();
                                services.AddSingleton<Messages>();
                                
                                // utilities
                                services.AddSingleton<GameScoreToTableConverter>();
                            })
                           .Build();
            return host;
        }
    }
}