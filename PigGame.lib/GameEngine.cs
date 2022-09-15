using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Enums;
using PigGame.lib.Exceptions;
using PigGame.lib.Models;
using PigGame.lib.Validators;

namespace PigGame.lib
{
    public class GameEngine
    {
        #region Fields

        private readonly int _diceCount;

        private readonly IDiceRollValidator _diceRollValidator;
        private readonly int _gameTargetScore;
        private int _currentPlayerNo;
        private int _playerRollCounter;

        #endregion

        #region Properties

        public Dictionary<int, List<int>> CurrentPlayerTurnRolls { get; } = new Dictionary<int, List<int>>();

        public Dictionary<PlayerModel, GameScoreModel> ScoreBoard { get; set; } = new Dictionary<PlayerModel, GameScoreModel>();
        public PlayerModel CurrentPlayer { get; private set; }

        public int CurrentTurnCounter { get; private set; }
        public DiceModel Dice { get; }

        #endregion

        #region Constructors

        public GameEngine(IDiceRollValidator diceRollValidator, DiceModel dice, int diceCount = 1, int gameTargetScore = 100)
        {
            _diceRollValidator = diceRollValidator;
            _diceCount = diceCount;
            _gameTargetScore = gameTargetScore;
            Dice = dice;
        }

        #endregion

        public void StartGame()
        {
            if (ScoreBoard.Count < 2) throw new ArgumentOutOfRangeException("Not enough players!");
            CurrentPlayer = ScoreBoard.Keys.First();
        }

        public void AddPlayer(PlayerModel player)
        {
            var gs = new GameScoreModel();
            ScoreBoard.Add(player, gs);
        }

        private void NextPlayer()
        {
            _currentPlayerNo++;

            if (_currentPlayerNo == ScoreBoard.Count)
            {
                _currentPlayerNo = 0;
                CurrentTurnCounter++;
            }

            CurrentPlayer = ScoreBoard.ElementAt(_currentPlayerNo)
                                      .Key;

            CurrentPlayerTurnRolls.Clear();
            _playerRollCounter = 0;
        }

        public void PlayerMove(PlayerAction action)
        {
            try
            {
                switch (action)
                {
                    case PlayerAction.ThrowDice:
                        _playerRollCounter++;
                        var rolls = PlayerRollDices()
                           .ToList();
                        CurrentPlayerTurnRolls.Add(_playerRollCounter, rolls);
                        break;
                    case PlayerAction.Hold:
                        ScoreBoard[CurrentPlayer]
                           .AddTurn(CurrentPlayerTurnRolls);
                        if (ScoreBoard[CurrentPlayer]
                               .GameScore() >= _gameTargetScore) throw new GameWonException($"{CurrentPlayer.Name} WON!!");
                        NextPlayer();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(action), action, null);
                }
            }
            catch (NoScoreTurnEndException e)
            {
                CurrentPlayer
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<int> PlayerRollDices()
        {
            for (var i = 0; i < _diceCount; i++) _diceRollValidator.AddDiceRoll(Dice.Roll());

            return _diceRollValidator.ValidateRolls();
        }
    }
}