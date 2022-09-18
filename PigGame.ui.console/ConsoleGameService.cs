using System;
using PigGame.lib;
using PigGame.lib.Enums;
using PigGame.lib.Exceptions;
using PigGame.ui.console.Exceptions;

namespace PigGame.ui.console
{
    public class ConsoleGameService
    {
        #region Static Fields and Const

        private const string ErrContinue = "Press any key to continue.";

        private const string ErrTryAgain = "Press any key to try again.";

        #endregion

        #region Fields

        private readonly GameEngine _gameEngine;
        private readonly Messages _messages;

        private bool _terminate;

        #endregion

        #region Constructors

        public ConsoleGameService(GameEngine gameEngine, Messages messages)
        {
            _gameEngine = gameEngine;
            _messages = messages;
        }

        #endregion

        public void Run()
        {
            while (!_terminate)
            {
                Console.Clear();
                Console.WriteLine(_messages.ShowGameLogo());
                Console.WriteLine(_messages.ShowGameSettingsAndMainMenu());
                Console.Write("Select: ");

                ActivateGameOption(Console.ReadLine());
            }
        }

        private void ActivateGameOption(string option)
        {
            while (!_terminate)
                try
                {
                    var selectedOption = GetMainMenuOption(option);
                    switch (selectedOption)
                    {
                        case MainMenuOption.AddPlayer:
                            OptionAddPlayer();
                            break;
                        case MainMenuOption.ExitGame:
                            _terminate = true;
                            break;
                        case MainMenuOption.StartGame:
                            _gameEngine.StartGame();
                            OptionStartGame();
                            break;
                        case MainMenuOption.ChangeDiceType:
                            OptionChangeDiceType();
                            break;
                        case MainMenuOption.ChangeGameMode:
                            OptionChangeGameMode();
                            break;
                        case MainMenuOption.ChangeWinScore:
                            OptionChangeWinScore();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    return;
                }
                catch (GameTerminatedException e)
                {
                    Console.WriteLine(e);
                    _terminate = true;
                }
                catch (NotEnoughPlayersException e)
                {
                    Console.WriteLine($"{e.Message} Press any key to continue.");
                    Console.ReadKey();
                    return;
                }
                catch (Exception e) when (e is ArgumentNullException || e is ArgumentOutOfRangeException || e is InvalidMenuOptionException)
                {
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        private void OptionAddPlayer()
        {
            var playerAdded = false;
            while (!playerAdded)
                try
                {
                    Console.Clear();
                    Console.WriteLine(_messages.ShowCurrentGameSettingPlayers());
                    Console.WriteLine("Add player.");

                    var playerNo = _gameEngine.Players.Count + 1;
                    Console.Write($"Enter Player {playerNo} name: ");
                    var playerName = Console.ReadLine();

                    _gameEngine.AddPlayer(playerName);

                    Console.WriteLine($"Player {playerName} added. Do you want to add another? Y/N: ");
                    var answer = Console.ReadLine();
                    playerAdded = answer?.ToUpper() != "Y";
                }
                catch (Exception e) when (e is PlayerNameNullException || e is PlayerNameIsTakenException)
                {
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    playerAdded = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        private void OptionStartGame()
        {
            var gameEnded = false;
            while (!gameEnded)
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Game Score Board:\n{_messages.PlayerTurnDetails()}\n");
                    Console.WriteLine(_messages.ShowPlayerActions());

                    var action = Console.ReadLine();
                    PlayerMove(action);
                }
                catch (InvalidPlayerActionException e)
                {
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                }
                catch (NoScoreTurnEndException e)
                {
                    Console.WriteLine($"{e.Message} {ErrContinue}");
                    Console.ReadKey();
                    _gameEngine.NextPlayer();
                }
                catch (MustRollCantHoldException e)
                {
                    Console.WriteLine($"{e.Message} {ErrContinue}");
                    Console.ReadKey();
                }
                catch (GameWonException e)
                {
                    gameEnded = true;
                    Console.Clear();
                    Console.WriteLine(_messages.ShowGameWon());
                    Console.WriteLine($"{e.Message} {ErrContinue}");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        private void PlayerMove(string action)
        {
            var moveExists = _messages.PlayerActions.TryGetValue(action.ToUpper(), out var playerMove);
            if (!moveExists) throw new InvalidPlayerActionException($"Selected action [{action}] is not valid.");

            _gameEngine.PlayerMove(playerMove);
        }

        private void OptionChangeDiceType()
        {
            var diceChanged = false;
            while (!diceChanged)
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Current {_messages.ShowCurrentGameSettingDiceType()}");
                    Console.WriteLine("Change Dice.");
                    Console.WriteLine(_messages.ShowDiceTypesList());
                    Console.Write("Select: ");

                    var newDiceType = Console.ReadLine();
                    var diceTypeExists = _messages.DiceTypes.TryGetValue(newDiceType, out var newDice);
                    if (!diceTypeExists) throw new ArgumentOutOfRangeException(nameof(newDiceType), $"There is no dice of type {newDiceType}");

                    _gameEngine.Settings.Dice.SetDiceType(newDice);

                    Console.WriteLine($"Dice type changed to {newDice}. Return to main menu? Y/N:");
                    var answer = Console.ReadLine();
                    diceChanged = answer?.ToUpper() == "Y";
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    diceChanged = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        private void OptionChangeGameMode()
        {
            var gameModeChanged = false;
            while (!gameModeChanged)
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Current: \n {_messages.ShowCurrentGameSettingGameMode()}");
                    Console.WriteLine("Change Game Mode.");
                    Console.WriteLine(_messages.ShowGameModesList());
                    Console.Write("Select: ");
                    var newGameMode = Console.ReadLine();
                    var gameModeExists = _messages.GameModes.TryGetValue(newGameMode, out var newMode);
                    if (!gameModeExists) throw new ArgumentOutOfRangeException(nameof(newGameMode), $"There is no game mode of type {newGameMode}");

                    _gameEngine.Settings.ChangeGameMode(newMode);

                    Console.WriteLine($"Game Mode changed to {newMode}. Return to main menu? Y/N:");
                    var answer = Console.ReadLine();
                    gameModeChanged = answer?.ToUpper() == "Y";
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    gameModeChanged = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        private void OptionChangeWinScore()
        {
            var winScoreChanged = false;
            while (!winScoreChanged)
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Current:\n{_messages.ShowCurrentGameSettingWinScore()}");
                    Console.WriteLine("Change Win Score.");
                    Console.Write("New Win Score: ");
                    var newWinScore = Console.ReadLine();
                    var canConver = int.TryParse(newWinScore, out var winScore);

                    if (!canConver) throw new ArgumentException("Incorrect value! New win score must be a number!");
                    _gameEngine.Settings.SetWinningScore(winScore);

                    Console.WriteLine($"Win score changed to {newWinScore}. Return to main menu? Y/N:");
                    var answer = Console.ReadLine();
                    winScoreChanged = answer?.ToUpper() == "Y";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    winScoreChanged = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        private MainMenuOption GetMainMenuOption(string selectedOption)
        {
            if (string.IsNullOrEmpty(selectedOption)) throw new ArgumentNullException(nameof(selectedOption), "No option selected!");
            if (selectedOption.Length > 1)
                throw new ArgumentOutOfRangeException(nameof(selectedOption),
                    $"Selected option length is not valid. Expected 1 character, user input length: {selectedOption.Length.ToString()}.");

            var selectedOptionFirstChar = selectedOption[0];

            var optionExist = _messages.MainMenuOptions.TryGetValue(char.ToUpper(selectedOptionFirstChar), out var option);
            if (optionExist && option == MainMenuOption.ExitGame) throw new GameTerminatedException("Exiting game!");
            if (optionExist) return option;

            throw new InvalidMenuOptionException($"Keys [{char.ToLower(selectedOptionFirstChar)}] or [{char.ToUpper(selectedOptionFirstChar)}] are not a valid options.");
        }
    }
}