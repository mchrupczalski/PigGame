using System.Collections.Generic;
using System.Linq;
using PigGame.lib;
using PigGame.lib.Enums;
using PigGame.ui.console.Utilities;

namespace PigGame.ui.console
{
    public class Messages
    {
        #region Fields

        private readonly GameEngine _gameEngine;
        private readonly GameScoreToTableConverter _gameScoreToTableConverter;

        #endregion

        #region Properties

        public Dictionary<char, MainMenuOption> MainMenuOptions { get; } = new Dictionary<char, MainMenuOption>
        {
            { 'A', MainMenuOption.AddPlayer },
            { 'G', MainMenuOption.ChangeGameMode },
            { 'D', MainMenuOption.ChangeDiceType },
            { 'W', MainMenuOption.ChangeWinScore },
            { 'S', MainMenuOption.StartGame },
            { 'E', MainMenuOption.ExitGame }
        };

        public Dictionary<string, DiceType> DiceTypes { get; } = new Dictionary<string, DiceType>
        {
            { "4", DiceType.D4 },
            { "6", DiceType.D6 },
            { "8", DiceType.D8 },
            { "10", DiceType.D10 },
            { "12", DiceType.D12 },
            { "20", DiceType.D20 }
        };

        public Dictionary<string, GameMode> GameModes { get; } = new Dictionary<string, GameMode>
        {
            { "N", GameMode.Normal },
            { "T", GameMode.TwoDice },
            { "TD", GameMode.TwoDiceWithDoubles },
            { "B", GameMode.BigPig },
            { "BD", GameMode.BigPigWithDoubles }
        };

        public Dictionary<string, PlayerAction> PlayerActions { get; } = new Dictionary<string, PlayerAction>
        {
            { "R", PlayerAction.Roll },
            { "H", PlayerAction.Hold }
        };

        #endregion

        #region Constructors

        public Messages(GameEngine gameEngine, GameScoreToTableConverter gameScoreToTableConverter)
        {
            _gameEngine = gameEngine;
            _gameScoreToTableConverter = gameScoreToTableConverter;
        }

        #endregion

        // @formatter:off — disable formatter after this line
        public string ShowGameLogo() =>
            @"           __,---.__            ____              " + "\n" + 
            @"        ,-'         `-.__      /\' .\    _____    " + "\n" + 
            @"      &/           `._\ _\    /: \___\  / .  /\   " + "\n" +
            @"      /               ''._    \' / . / /____/..\  " + "\n" + 
            @"      |   ,             ("")    \/___/  \'  '\  /  " + "\n" + 
            @"      |__,'`-..--|__|--''               \'__'\/   " + "\n\n" +
            @"            --- WELCOME TO THE PIG GAME ---       " + "\n";
        // @formatter:on — enable formatter after this line

        // @formatter:off — disable formatter after this line
        public string ShowGameWon() =>
            @"                 _                           _             " + "\n" +
            @"                 ;`.                       ,'/             " + "\n" +
            @"                 |`.`-.      _____      ,-;,'|             " + "\n" +
            @"                 |  `-.\__,-'     `-.__//'   |             " + "\n" +
            @"                 |     `|               \ ,  |             " + "\n" +
            @"                 `.  ```                 ,  .'             " + "\n" +
            @"                   \_`      .     ,   ,  `_/               " + "\n" +
            @"                     \    ^  `   ,   ^ ` /                 " + "\n" +
            @"                      | '  |  ____  | , |                  " + "\n" +
            @"                      |     ,'    `.    |                  " + "\n" +
            @"                      |    (  O' O  )   |                  " + "\n" +
            @"                      `.    \__,.__/   ,'                  " + "\n" +
            @"                        `-._  `--'  _,'                    " + "\n" +
            @"                            `------'                       " + "\n" +
            @"    _____   ___  ___  ___ _____   _____  _   _ ___________ " + "\n" +
            @"   |  __ \ / _ \ |  \/  ||  ___| |  _  || | | |  ___| ___ \" + "\n" +
            @"   | |  \// /_\ \| .  . || |__   | | | || | | | |__ | |_/ /" + "\n" +
            @"   | | __ |  _  || |\/| ||  __|  | | | || | | |  __||    / " + "\n" +
            @"   | |_\ \| | | || |  | || |___  \ \_/ /\ \_/ / |___| |\ \ " + "\n" +
            @"    \____/\_| |_/\_|  |_/\____/   \___/  \___/\____/\_| \_|"+ "\n" ;
        // @formatter:on — enable formatter after this line

        public string ShowMainMenu()
        {
            return $" Main Menu: \n{string.Join("\n", MainMenuOptions.Select(o => $"  {o.Key.ToString()}: {o.Value.ToString()}"))}\n";
        }

        public string ShowCurrentGameSettings()
        {
            var players = ShowCurrentGameSettingPlayers();
            var gameMode = ShowCurrentGameSettingGameMode();
            var diceType = ShowCurrentGameSettingDiceType();
            var winScore = ShowCurrentGameSettingWinScore();

            return $" Game Settings:\n{players}\n{gameMode}\n{diceType}\n{winScore}\n";
        }

        public string ShowCurrentGameSettingPlayers()
        {
            var playersCount = _gameEngine.Players.Count;
            var playersNames = playersCount == 0 ? "There is not players" : string.Join(", ", _gameEngine.Players);
            var players = $"  {playersCount.ToString()} Players: ({playersNames})";
            return players;
        }

        public string ShowCurrentGameSettingGameMode()
        {
            var gameMode = $"Game Mode: {_gameEngine.Settings.GameMode.ToString()}";
            var diceCount = $"Number of dices: {_gameEngine.Settings.DiceCount.ToString()}";
            return $"  {gameMode}\n  {diceCount}";
        }

        public string ShowCurrentGameSettingDiceType() => $"  Dice Type: {_gameEngine.Settings.Dice.DiceType.ToString()}";

        public string ShowCurrentGameSettingWinScore() => $"  Win Score: {_gameEngine.Settings.WinScore.ToString()}";

        public string ShowDiceTypesList() => $"Dice types: \n {string.Join("\n", DiceTypes.Select(d => $"{d.Key} : {d.Value}"))}";

        public string ShowGameModesList() => $"Game modes: \n {string.Join("\n", GameModes.Select(d => $"{d.Key} : {d.Value}"))}";

        public string ShowGameSettingsAndMainMenu() => $"{ShowCurrentGameSettings()}\n\n{ShowMainMenu()}";

        public string PlayerTurnDetails()
        {
            var scoreBoard = ShowScoreBoard();
            var playerInfo = $"Current Player: {_gameEngine.CurrentPlayer.Name}\nPlayer Turn: {_gameEngine.CurrentPlayer.Turns.TurnCounter.ToString()}";
            var playerRolls = $"Current turn rolls:\n{_gameEngine.CurrentPlayer.Turns.CurrentTurnRolls}";
            return $"{scoreBoard}\n\n{playerInfo}\n\n{playerRolls}";
        }

        public string ShowScoreBoard() => _gameScoreToTableConverter.Convert(_gameEngine.Players);

        public string ShowPlayerActions() =>
            $"{_gameEngine.CurrentPlayer.Name} it is your move. What do you want to do?\n{string.Join("\n", PlayerActions.Select(a => $"[{a.Key}] to {a.Value}"))}";
    }
}