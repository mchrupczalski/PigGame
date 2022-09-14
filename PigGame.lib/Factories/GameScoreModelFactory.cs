using PigGame.lib.Models;

namespace PigGame.lib.Factories
{
    public class GameScoreModelFactory
    {
        private readonly GameTurnModelSingleDiceFactory _gameTurnModelSingleDiceFactory;

        public GameScoreModelFactory(GameTurnModelSingleDiceFactory gameTurnModelSingleDiceFactory)
        {
            _gameTurnModelSingleDiceFactory = gameTurnModelSingleDiceFactory;
        }

        public GameScoreModel CreateGameScoreModel() => new GameScoreModel(_gameTurnModelSingleDiceFactory);
    }
}