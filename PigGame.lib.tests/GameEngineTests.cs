using System;
using System.Collections.Generic;
using PigGame.lib.Enums;
using PigGame.lib.Exceptions;
using PigGame.lib.Factories;
using PigGame.lib.Models;
using PigGame.lib.Validators;
using Xunit;

namespace PigGame.lib.tests
{
    public class GameEngineTests
    {
        [Fact]
        public void ValidateRolls_TwoDiceModeAfterThreeTurnsPlayerRolledDoubleOnes_ScoreResetsToZero()
        {
            var engine = GameEngineDefaultConstructor(GameMode.TwoDice);
            var rollOne = new List<int>() { 2, 3 };
            var rollTwo = new List<int>() { 3, 4 };
            var rollThree = new List<int>() { 5, 6 };
            var rollFour = new List<int>() { 1, 1 };
            
            engine.AddPlayer("Player1");
            engine.AddPlayer("Player2");
            engine.StartGame();
            
            engine.CurrentPlayer.Turns.CurrentTurnRolls.AddDicesRoll(rollOne);
            engine.CurrentPlayer.Turns.NextTurn();
            
            engine.CurrentPlayer.Turns.CurrentTurnRolls.AddDicesRoll(rollTwo);
            engine.CurrentPlayer.Turns.NextTurn();
            
            engine.CurrentPlayer.Turns.CurrentTurnRolls.AddDicesRoll(rollThree);
            engine.CurrentPlayer.Turns.NextTurn();
            
            engine.CurrentPlayer.Turns.CurrentTurnRolls.AddDicesRoll(rollFour);
            try
            {
                engine.ValidateRolls(rollFour);
            }
            catch (DoubleOnesRolledScoreLostTurnEndException)
            {
                Assert.Equal(0, engine.CurrentPlayer.Turns.GameScore());
            }
            
        }
        private GameEngine GameEngineDefaultConstructor(GameMode gameMode)
        {
            var diceRollModelCreator = new TurnModel.DiceRollModelCreator(() => new DiceRollModel());
            var turnCreator = new PlayerFactory.TurnCreator(() => new TurnModel(diceRollModelCreator));
            var playerFactory = new PlayerFactory(turnCreator);
            var dice = new DiceModel();
            var validatorResolver = new GameSettingsModel.DiceRollValidatorResolver((key) =>
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
            var gameSettings = new GameSettingsModel(dice, validatorResolver);
            gameSettings.ChangeGameMode(gameMode);

            return new GameEngine(playerFactory, gameSettings);
        }
    }
}