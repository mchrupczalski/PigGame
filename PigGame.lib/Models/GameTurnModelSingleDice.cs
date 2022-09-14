using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Models
{
    public class GameTurnModelSingleDice
    {
        private readonly DiceModel _dice;
        private readonly Dictionary<int,int> _diceRolls = new Dictionary<int, int>();
        
        private int _throws;
        
        public GameTurnModelSingleDice(DiceModel dice)
        {
            _dice = dice ?? throw new ArgumentNullException(nameof(dice));
            _throws = 0;
        }

        public void NextThrow()
        {
            _throws++;
            var roll = _dice.Roll();

            if (roll == 1) throw new ThrowLostException();
            
            _diceRolls.Add(_throws,roll);
        }

        public int ThrowsScore() => _diceRolls.Values.Sum();

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString() => ThrowsHistory();
        
        /// <summary>
        ///     Turn Dice rolls history
        /// </summary>
        /// <returns>
        ///     Returns a string representing dice value for each roll in this turn
        /// </returns>
        public string ThrowsHistory() => string.Join(" - ", $"({_diceRolls.Values.Distinct()})");

        #endregion
    }
}