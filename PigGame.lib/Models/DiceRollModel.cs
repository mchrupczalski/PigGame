using System.Collections.Generic;
using System.Linq;

namespace PigGame.lib.Models
{
    /// <summary>
    ///     Holds a list of rolls within a single Turn
    /// </summary>
    public class DiceRollModel
    {
        #region Fields

        /// <summary>
        ///     Holds bonus value for a specific roll number
        /// </summary>
        private readonly Dictionary<int, int> _rollBonuses = new Dictionary<int, int>();

        /// <summary>
        ///     Holds all rolls in turn
        /// </summary>
        private readonly Dictionary<int, List<int>> _rolls = new Dictionary<int, List<int>>();

        /// <summary>
        ///     Number of rolls in turn
        /// </summary>
        private int _rollCounter = 1;

        /// <summary>
        ///     Used to set player score and override sum of dice rolls
        /// </summary>
        private int? _scoreOverride;

        #endregion

        #region Properties

        /// <summary>
        ///     Prevents player from holding turn.
        /// </summary>
        public bool MustRoll { get; set; }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            const string noRolls = "You haven't rolled yet in this turn.";
            return _rolls.Count == 0
                ? noRolls
                : $"{string.Join("\n", _rolls.Select(x => $"R{x.Key.ToString()} : ({string.Join(",", x.Value)}) {ToStringBonus(x.Key)}"))}\nTurn Score: {RollsScore().ToString()}";
        }

        #endregion

        /// <summary>
        ///     Checks if bonus for given roll exists
        /// </summary>
        /// <param name="rollNo">Roll Number to check for bonus</param>
        /// <returns>String representing the bonus or an empty string if no bonus for given roll</returns>
        private string ToStringBonus(int rollNo) => _rollBonuses.ContainsKey(rollNo) ? $"Bonus:  {_rollBonuses[rollNo].ToString()}" : string.Empty;

        /// <summary>
        ///     Adds dices roll
        /// </summary>
        /// <param name="rolls">List of dice(s) roll values</param>
        public void AddDicesRoll(IEnumerable<int> rolls)
        {
            _rolls.Add(NextRoll(), rolls.ToList());
            MustRoll = false;
        }

        /// <summary>
        ///     Increments the roll counter
        /// </summary>
        /// <returns></returns>
        private int NextRoll() => _rollCounter++;

        /// <summary>
        ///     Used to add bonus points to the current roll
        /// </summary>
        /// <param name="bonus"></param>
        public void AddRollBonus(int bonus) => _rollBonuses.Add(_rollCounter, bonus);

        /// <summary>
        ///     For certain game mode, score reset is required
        /// </summary>
        /// <param name="scoreOverrideValue"></param>
        public void OverrideScore(int scoreOverrideValue) => _scoreOverride = scoreOverrideValue;

        /// <summary>
        ///     Sum of all rolls within a Turn
        /// </summary>
        /// <returns>Returns a sum of all rolls within a Turn or Overriden Score (if set)</returns>
        public int RollsScore() => _scoreOverride ?? _rolls.Sum(d => d.Value.Sum()) + _rollBonuses.Sum(d => d.Value);
    }
}