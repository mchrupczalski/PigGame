using PigGame.lib.Models;

namespace PigGame.lib.Factories
{
    public class GameTurnModelSingleDiceFactory
    {
        #region Fields

        private readonly DiceModel _dice;

        #endregion

        #region Constructors

        public GameTurnModelSingleDiceFactory(DiceModel dice)
        {
            _dice = dice;
        }

        #endregion

        public GameTurnModelSingleDice CreateGameTurn() => new GameTurnModelSingleDice(_dice);
    }
}