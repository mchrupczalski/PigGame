using System;
using PigGame.lib.Enums;

namespace PigGame.lib.Models
{
    public class DiceModel
    {
        #region Static Fields and Const

        private const int _diceMin = 1;
        private static readonly Random Random = new Random();

        #endregion

        #region Fields

        private int _diceMax;

        #endregion

        #region Properties

        public DiceType DiceType { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates new dice instance
        /// </summary>
        /// <param name="diceType">Type of dice to play with</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if dice value is less than 6</exception>
        public DiceModel(DiceType diceType = DiceType.D6)
        {
            DiceType = diceType;
            SetDiceType(diceType);
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => DiceType.ToString();

        #endregion

        /// <summary>
        ///     Changes the max value that can be rolled on the dice
        /// </summary>
        /// <param name="diceType">Dice type</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetDiceType(DiceType diceType)
        {
            switch (diceType)
            {
                case DiceType.D4:
                    _diceMax = 4;
                    break;
                case DiceType.D6:
                    _diceMax = 6;
                    break;
                case DiceType.D8:
                    _diceMax = 8;
                    break;
                case DiceType.D10:
                    _diceMax = 10;
                    break;
                case DiceType.D12:
                    _diceMax = 12;
                    break;
                case DiceType.D20:
                    _diceMax = 20;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(diceType), diceType, null);
            }

            DiceType = diceType;
        }

        public int Roll() => Random.Next(_diceMin, _diceMax);
    }
}