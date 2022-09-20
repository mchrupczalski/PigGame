using System;
using PigGame.lib.Factories;
using PigGame.lib.Models;
using Xunit;

namespace PigGame.lib.tests.Factories
{
    public class PlayerFactoryTests
    {
        [Fact]
        public void Create_NoNamePassed_ThrowsException()
        {
            var factory = PlayerFactoryDefaultConstructor();
            Assert.Throws<ArgumentNullException>(() => factory.Create(""));
        }

        [Fact]
        public void Create_NamePassed_CreatesNewPlayer()
        {
            const string testName = "NewName"; 
            
            var factory = PlayerFactoryDefaultConstructor();
            var player = factory.Create(testName);
            
            Assert.Equal(testName, player.Name);
        }
        
        private PlayerFactory PlayerFactoryDefaultConstructor()
        {
            var diceRollModelCreator = new TurnModel.DiceRollModelCreator(() => new DiceRollModel());
            var turnCreator = new PlayerFactory.TurnCreator(() => new TurnModel(diceRollModelCreator));
            return new PlayerFactory(turnCreator);
        }
    }
}