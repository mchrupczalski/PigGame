using System.Collections.Generic;
using PigGame.lib.Exceptions;
using PigGame.lib.Factories;

namespace PigGame.lib.Models
{
    public class GameScoreModel
    {
        #region Fields

        private readonly int _gameTargetScore;

        private readonly GameTurnModelSingleDiceFactory _turnFactory;

        private readonly Dictionary<int, GameTurnModelSingleDice> _turns = new Dictionary<int, GameTurnModelSingleDice>();

        public GameTurnModelSingleDice _currentTurn;
        private bool _turnActive;
        private int _turnCounter;

        #endregion

        #region Properties

        public int GameScore { get; private set; }

        #endregion

        #region Constructors

        public GameScoreModel(GameTurnModelSingleDiceFactory turnFactory, int gameTargetScore = 100)
        {
            _turnFactory = turnFactory;
            _gameTargetScore = gameTargetScore;
            _currentTurn = _turnFactory.CreateGameTurn();
        }

        #endregion

        public void NextTurn()
        {
            _turnCounter++;
            _turnActive = true;
            _currentTurn = _turnFactory.CreateGameTurn();
        }

        public void PlayTurn()
        {
            try
            {
                if (!_turnActive) throw new TurnEndedException();

                _currentTurn.NextThrow();
                if (GameScore + _currentTurn.ThrowsScore() == _gameTargetScore) throw new GameWonException();
            }
            catch (ThrowLostException e)
            {
                _turnActive = false;
                throw new TurnEndedException();
            }
        }

        public void HoldTurn()
        {
            _turns.Add(_turnCounter, _currentTurn);
            GameScore += _currentTurn.ThrowsScore();
            _turnActive = false;
        }
    }
}