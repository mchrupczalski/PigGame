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

        private const string ErrContinue = "\nPress any key to continue.";

        private const string ErrTryAgain = "\nPress any key to try again.";

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
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(_messages.ShowGameLogo());
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(_messages.ShowCurrentGameSettings());
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine(_messages.ShowMainMenu());
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  Select: ");
                Console.ResetColor();

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
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} Press any key to continue.");
                    Console.ReadKey();
                    Console.ResetColor();
                    return;
                }
                catch (Exception e) when (e is ArgumentNullException || e is ArgumentOutOfRangeException || e is InvalidMenuOptionException)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    Console.Clear();
                    Console.ResetColor();
                    return;
                }
                catch (Exception e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine(e);
                    Console.ResetColor();
                    throw;
                }
        }

        private void SetConsoleForErrorDisplay() => Console.ForegroundColor = ConsoleColor.DarkRed;
        private void SetConsoleForSuccessDisplay() => Console.ForegroundColor = ConsoleColor.Green;

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

                    SetConsoleForSuccessDisplay();
                    Console.WriteLine($"Player {playerName} added.\nDo you want to add another player? Y/N: ");
                    Console.ResetColor();
                    var answer = Console.ReadLine();
                    playerAdded = answer?.ToUpper() != "Y";
                }
                catch (Exception e) when (e is PlayerNameNullException || e is PlayerNameIsTakenException)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    Console.ResetColor();
                    playerAdded = false;
                }
                catch (Exception e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine(e);
                    Console.ResetColor();
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
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    Console.ResetColor();
                }
                catch (NoScoreTurnEndException e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrContinue}");
                    Console.ReadKey();
                    Console.ResetColor();
                    _gameEngine.NextPlayer();
                }
                catch (MustRollCantHoldException e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrContinue}");
                    Console.ReadKey();
                    Console.ResetColor();
                }
                catch (GameWonException e)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    gameEnded = true;
                    Console.Clear();
                    Console.WriteLine(_messages.ShowGameWon());
                    Console.WriteLine($"      {e.Message}");
                    Console.WriteLine($"\nScore Board:\n{_messages.ShowScoreBoard()}");
                    Console.WriteLine($"{ErrContinue}");
                    Console.ReadKey();
                    Console.ResetColor();
                    _gameEngine.ResetPlayersGame();
                }
                catch (Exception e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine(e);
                    Console.ResetColor();
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

                    SetConsoleForSuccessDisplay();
                    Console.WriteLine($"Dice type changed to {newDice}.\nReturn to main menu? Y/N:");
                    Console.ResetColor();
                    var answer = Console.ReadLine();
                    diceChanged = answer?.ToUpper() == "Y";
                }
                catch (ArgumentOutOfRangeException e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    Console.ResetColor();
                    diceChanged = false;
                }
                catch (Exception e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine(e);
                    Console.ResetColor();
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

                    SetConsoleForSuccessDisplay();
                    Console.WriteLine($"Game Mode changed to {newMode}.\nReturn to main menu? Y/N:");
                    Console.ResetColor();
                    var answer = Console.ReadLine();
                    gameModeChanged = answer?.ToUpper() == "Y";
                }
                catch (ArgumentOutOfRangeException e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    Console.ResetColor();
                    gameModeChanged = false;
                }
                catch (Exception e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine(e);
                    Console.ResetColor();
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

                    SetConsoleForSuccessDisplay();
                    Console.WriteLine($"Win score changed to {newWinScore}.\nReturn to main menu? Y/N:");
                    Console.ResetColor();
                    var answer = Console.ReadLine();
                    winScoreChanged = answer?.ToUpper() == "Y";
                }
                catch (ArgumentException e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine($"{e.Message} {ErrTryAgain}");
                    Console.ReadKey();
                    Console.ResetColor();
                    winScoreChanged = false;
                }
                catch (Exception e)
                {
                    SetConsoleForErrorDisplay();
                    Console.WriteLine(e);
                    Console.ResetColor();
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