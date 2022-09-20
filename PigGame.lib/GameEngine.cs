using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Enums;
using PigGame.lib.Exceptions;
using PigGame.lib.Factories;
using PigGame.lib.Models;

namespace PigGame.lib
{
    public class GameEngine
    {
        #region Fields

        /// <summary>
        ///     A Factory Object responsible for creating new players
        /// </summary>
        private readonly PlayerFactory _playerFactory;

        /// <summary>
        ///     An integer representing list index of the current player
        /// </summary>
        private int _currentPlayerNo;

        #endregion

        #region Properties

        /// <summary>
        ///     An object holding game settings. <see cref="GameSettingsModel" />
        /// </summary>
        public GameSettingsModel Settings { get; }

        /// <summary>
        ///     A list of Players
        /// </summary>
        public List<PlayerModel> Players { get; set; } = new List<PlayerModel>();

        /// <summary>
        ///     An object representing the active player
        /// </summary>
        public PlayerModel CurrentPlayer { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of <see cref="GameEngine" />
        /// </summary>
        /// <param name="playerFactory">A Factory object responsible for creating new players</param>
        /// <param name="settings">An object representing the game settings</param>
        public GameEngine(PlayerFactory playerFactory, GameSettingsModel settings)
        {
            Settings = settings;
            _playerFactory = playerFactory;
        }

        #endregion

        /// <summary>
        ///     Starts the game
        /// </summary>
        /// <exception cref="NotEnoughPlayersException">Thrown if not enough players</exception>
        public void StartGame()
        {
            if (Players.Count < 2) throw new NotEnoughPlayersException("Not enough players!");
            CurrentPlayer = Players[0];
            CurrentPlayer.Turns.NextTurn();
        }

        /// <summary>
        ///     Adds Player to the game
        /// </summary>
        /// <param name="name">A new player name</param>
        /// <exception cref="PlayerNameIsTakenException">Thrown if a player with the same name already exists</exception>
        public void AddPlayer(string name)
        {
            if (Players.FirstOrDefault(p => p.Name == name) != null) throw new PlayerNameIsTakenException($"{name} is already taken, please try a different name.");
            if (string.IsNullOrEmpty(name)) throw new PlayerNameNullException("Player name cannot be empty!");
            Players.Add(_playerFactory.Create(name));
        }

        /// <summary>
        ///     Starts next player turn
        /// </summary>
        public void NextPlayer()
        {
            _currentPlayerNo++;

            if (_currentPlayerNo == Players.Count) _currentPlayerNo = 0;
            CurrentPlayer = Players[_currentPlayerNo];
            CurrentPlayer.Turns.NextTurn();
        }

        /// <summary>
        ///     An action taken by the current player
        /// </summary>
        /// <param name="action"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void PlayerMove(PlayerAction action)
        {
            switch (action)
            {
                case PlayerAction.Roll:
                    var rolls = PlayerRollDice();
                    CurrentPlayer.Turns.CurrentTurnRolls.AddDicesRoll(rolls);
                    ValidateRolls(rolls);
                    break;
                case PlayerAction.Hold:
                    PlayerHold();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        public void ValidateRolls(IEnumerable<int> rolls)
        {
            try
            {
                Settings.DiceRollValidator.RollIsValid(rolls);
            }
            catch (OneRolledNoScoreTurnEndException)
            {
                // reset any score for this turn to 0
                CurrentPlayer.Turns.CurrentTurnRolls.OverrideScore(0);
                CurrentPlayer.Turns.EndTurn();
                throw;
            }
            // ReSharper disable once RedundantCatchClause
            catch (DoubleRolledMustRollCantHoldException)
            {
                CurrentPlayer.Turns.CurrentTurnRolls.MustRoll = true;
                throw;
            }
            catch (DoubleOnesRolledScoreLostTurnEndException)
            {
                var totalScore = CurrentPlayer.Turns.GameScore(true);
                CurrentPlayer.Turns.CurrentTurnRolls.OverrideScore((totalScore * -1)+2);
                CurrentPlayer.Turns.EndTurn();
                throw;
            }
            catch (DoubleOnesRolledAddsBonusException)
            {
                CurrentPlayer.Turns.CurrentTurnRolls.AddRollBonus(25);
                throw;
            }
            catch (DoubleRolledDoubleTheRollBonusException)
            {
                var bonus = CurrentPlayer.Turns.CurrentTurnRolls.LatestRolls.Sum() * 2;
                CurrentPlayer.Turns.CurrentTurnRolls.AddRollBonus(bonus);
                throw;
            }
        }

        /// <summary>
        ///     Resets the game, use before starting a new game
        /// </summary>
        public void ResetPlayersGame()
        {
            Players.ForEach(p => p.Turns.ResetTurns());
        }

        /// <summary>
        ///     Logic for player rolling the dice
        /// </summary>
        public List<int> PlayerRollDice()
        {
            var rolls = new List<int>();
            for (var i = 0; i < Settings.DiceCount; i++) rolls.Add(Settings.Dice.Roll());
            return rolls;
        }

        /// <summary>
        ///     Checks if player has won the game
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GameWonException">Thrown if player reached the game score</exception>
        public bool PlayerWon()
        {
            var won = CurrentPlayer.Turns.GameScore(true) >= Settings.WinScore;

            if (!won) return false;

            CurrentPlayer.Turns.EndTurn();
            throw new GameWonException($"{CurrentPlayer.Name} Won the game with score of : {CurrentPlayer.Turns.GameScore(true)}");
        }

        /// <summary>
        ///     Player holds current turn
        /// </summary>
        /// <exception cref="DoubleRolledMustRollCantHoldException">
        ///     Depending on Roll Validator, player may be blocked from holding, must roll
        ///     again
        /// </exception>
        public void PlayerHold()
        {
            if (CurrentPlayer.Turns.CurrentTurnRolls.MustRoll) throw new DoubleRolledMustRollCantHoldException($"{CurrentPlayer.Name} cannot hold. Must roll again.");
            CurrentPlayer.Turns.EndTurn();
            NextPlayer();
        }
    }
}