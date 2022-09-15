using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Validators;

namespace PigGame.lib.Models
{
    public class DiceRollModel
    {
        #region Fields

        private readonly Dictionary<int, List<int>> _diceRolls = new Dictionary<int, List<int>>();

        private readonly IDiceRollValidator _rollValidator;

        #endregion

        #region Properties

        public bool RollIsValid { get; private set; }

        #endregion

        #region Constructors

        public DiceRollModel(IDiceRollValidator rollValidator)
        {
            _rollValidator = rollValidator;
        }

        #endregion

        public void AddDiceRolls(IEnumerable<int> diceRolls)
        {
            var rollNo = _diceRolls.Count + 1;
            _diceRolls.Add(rollNo, );

            rolls.AddRange(diceRolls);
            RollIsValid = _rollValidator.RollIsValid(rolls);
        }

        public int RollScore() => RollIsValid ? _diceRolls.Values.Sum(d => d.Sum()) : 0;

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            var d = new Dictionary<int, List<int>>()
            {
                {1, new List<int>(){1,2,3}},
                {2, new List<int>(){1,2,3}},
            };

            var p = string.Join("\n", d.Select(x => $"{x.Key.ToString()} -> ({string.Join(", ", d[x.Key])}) = {}"));
            return $"({RollNo.ToString()} -> {string.Join("), (", _diceRolls)}:{RollScore().ToString()})";
        }

        #endregion
    }
}