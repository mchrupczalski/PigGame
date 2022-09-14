using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib;
using PigGame.lib.Enums;

namespace PigGame.ui.console
{
    public class ConsoleGame
    {
        #region Fields

        private readonly Game _game;

        private readonly Dictionary<char, GameOption> _gameOptions = new Dictionary<char, GameOption>
        {
            { 'A', GameOption.AddPlayer },
            { 'P', GameOption.ShowPlayers },
            { 'R', GameOption.RestartGame },
            { 'S', GameOption.StartGame },
            { 'E', GameOption.ExitGame },
        };

        #endregion

        #region Constructors

        public ConsoleGame(Game game)
        {
            _game = game;
        }

        #endregion

        public void Play()
        {
            var selectedOption = GetGameOptionUserSelection();
            var validSelection = false;

            while (!validSelection)
                switch (selectedOption)
                {
                    case GameOption.AddPlayer:
                        validSelection = true;
                        OptionAddPlayer();
                        break;
                    case GameOption.ExitGame:
                        validSelection = true;
                        return;
                    case GameOption.RestartGame:
                        validSelection = true;
                        break;
                    case GameOption.StartGame:
                        validSelection = true;
                        break;
                    case GameOption.ShowPlayers:
                        OptionShowPlayers();
                        validSelection = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        private GameOption GetGameOptionUserSelection()
        {
            var optionExist = false;

            while (!optionExist)
            {
                Console.Clear();
                Console.WriteLine("Welcome To PigGame!");
                OptionShowPlayers();
                
                Console.WriteLine("Select option:");
                foreach (var gameOption in _gameOptions) Console.WriteLine($"{gameOption.Key.ToString()}: {gameOption.Value.ToString()}");

                var selection = Console.ReadKey()
                                       .KeyChar;
                optionExist = _gameOptions.TryGetValue(char.ToUpper(selection), out var option);
                if (optionExist) return option;

                Console.WriteLine();
                Console.WriteLine($"Keys [{selection}] or [{char.ToUpper(selection)}] are not a valid options. Press any key to try again.");
                Console.ReadKey();
            }

            throw new ArgumentOutOfRangeException("Game Option");
        }

        private void OptionShowPlayers()
        {
            var players = "No players, add some!";
            if (_game.Engine.Players.Count > 0) players = string.Join(", ", _game.Engine.Players);
            Console.WriteLine($"Current players: {players}");
        }

        private void OptionAddPlayer()
        {
            var nameAvailable = false;
            var playerName = string.Empty;
            
            while (!nameAvailable)
            {
                var playerNo = _game.Engine.Players.Count + 1;
                Console.WriteLine($"Enter Player {playerNo} name: ");
                
                while (playerName.Length == 0)
                {
                    playerName = Console.ReadLine();
                    if(playerName.Length == 0)Console.WriteLine($"Player name cannot be empty!");
                }
                
                nameAvailable = string.IsNullOrEmpty(_game.Engine.Players.FirstOrDefault(p => p.Name == playerName).ToString());

                if (!nameAvailable)
                {
                    Console.WriteLine($"Whoops! That name is already taken, try another one.");
                }
            }
            
            _game.AddPlayer(playerName);
        }
    }
}