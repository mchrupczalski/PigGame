using System.Collections.Generic;
using System.Linq;

namespace PigGame.lib.Models
{
    public class GameScoreModel
    {
        #region Fields

        // dictionary of turn > roll count > dice roll values
        private readonly List<Dictionary<int,List<int>>> _turns = new List<Dictionary<int, List<int>>>();

        #endregion

        #region Constructors

        #endregion

        public void AddTurn(Dictionary<int,List<int>> rolls) => _turns.Add(rolls);

        public int GameScore() => _turns.Sum(d => d.Values.Sum(x => x.Sum()));
    }
}