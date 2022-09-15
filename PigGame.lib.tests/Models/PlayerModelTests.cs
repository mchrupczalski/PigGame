using System;
using PigGame.lib.Models;
using Xunit;

namespace PigGame.lib.tests.Models
{
    public class PlayerModelTests
    {
        [Theory]
        [InlineData("")]
        public void CreatingPlayerWithNoNameThrowsException(string name) => Assert.Throws<ArgumentNullException>(() => _ = new PlayerModel(name));
    }
}