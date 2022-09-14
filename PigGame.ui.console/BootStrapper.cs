using DryIoc;
using PigGame.lib;
using PigGame.lib.Factories;
using PigGame.lib.Models;

namespace PigGame.ui.console
{
    public class BootStrapper
    {
        public IContainer Bootstrap()
        {
            var container = new Container();
            
            container.Register<DiceModel>(Reuse.Singleton);
            container.Register<GameScoreModel>();
            container.Register<GameTurnModelSingleDice>();
            container.Register<GameTurnModelSingleDiceFactory>(Reuse.Singleton);
            container.Register<GameScoreModelFactory>(Reuse.Singleton);
            container.Register<GameEngine>();
            container.Register<Game>();

            return container;
        }
    }
}