using System;

namespace PigGame.lib.Models
{
    public class DiceModel
    {
        private const int _diceMin = 1;
        private readonly int _diceMax;
        private static readonly Random Random = new Random();

        /// <summary>
        ///     Creates new dice instance
        /// </summary>
        /// <param name="diceMax">Max value for dice</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if dice value is less than 6</exception>
        public DiceModel(int diceMax = 6)
        {
            if (diceMax <= 5) throw new ArgumentOutOfRangeException(nameof(diceMax));
            _diceMax = diceMax;
        }

        public int Roll() => Random.Next(_diceMin, _diceMax);
    }
}