using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Models;

namespace PigGame.ui.console.Utilities
{
    public class GameScoreToTableConverter
    {
        #region Static Fields and Const

        private const string nameColHeader = "Players";
        private const string scoreColHeader = "Score";
        private const string turnColHeader = "Turn";

        #endregion

        #region Fields

        private bool _initialized;

        private int nameColCharsCount = 0;
        private int scoreColCharsCount = 0;
        private int turnColCharsCount = 0;

        #endregion

        private void Initialize(IEnumerable<PlayerModel> players)
        {
            const int extraSpace = 2;
            
            var names = players.Select(p => p.Name);
            var nameMax = names.Max(n => n).Length;
            nameColCharsCount = nameMax < nameColHeader.Length ? nameColHeader.Length : nameMax;

            nameColCharsCount += extraSpace;
            turnColCharsCount = turnColHeader.Length + extraSpace;
            scoreColCharsCount = scoreColHeader.Length + extraSpace;
        }

        public string Convert(IEnumerable<PlayerModel> players)
        {
            if (!_initialized) Initialize(players);

            var header = CreateHeaderRow();
            var rows = new List<string>();
            foreach (var player in players)
            {
                var rowPlayer = CreateCellAlignCenter(player.Name,                       nameColCharsCount, true);
                var rowTurn = CreateCellAlignCenter(player.Turns.TurnCounter.ToString(), turnColCharsCount);
                var rowScore = CreateCellAlignCenter(player.Turns.GameScore()
                                                           .ToString(), scoreColCharsCount);
                rows.Add($"{rowPlayer}{rowTurn}{rowScore}");
            }

            return $"{header}\n{string.Join("\n", rows)}";
        }

        private string CreateHeaderRow()
        {
            const char rowSeparator = '-';
            
            var hPlayers = CreateCellAlignCenter(nameColHeader, nameColCharsCount, true);
            var hTurn = CreateCellAlignCenter(turnColHeader,    turnColCharsCount);
            var hScore = CreateCellAlignCenter(scoreColHeader,  scoreColCharsCount);

            var sPlayers = CreateCellAlignCenter(new string(rowSeparator, nameColCharsCount),  nameColCharsCount, true);
            var sTurn = CreateCellAlignCenter(new string(rowSeparator,    turnColCharsCount),  turnColCharsCount);
            var sScore = CreateCellAlignCenter(new string(rowSeparator,   scoreColCharsCount), scoreColCharsCount);

            return $"{hPlayers}{hTurn}{hScore}\n{sPlayers}{sTurn}{sScore}";
        }

        private string CreateCellAlignCenter(string cellValue, int cellCharsCount, bool borderLeft = false)
        {
            const string border = "|";
            
            var freeSpace = cellCharsCount - cellValue.Length;
            if (freeSpace < 0) throw new ArgumentOutOfRangeException(nameof(cellCharsCount), $"Specified {nameof(cellCharsCount)} Value is less than length of {nameof(cellValue)}.");

            var spaceEven = freeSpace % 2 == 0;
            var spacing = freeSpace / 2;
            var spacingLeft = new string(' ', spacing);
            var spacingRight = new string(' ', spaceEven ? spacing : spacing + 1);

            var output = borderLeft ? border : string.Empty;
            output += $"{spacingLeft}{cellValue}{spacingRight}{border}";

            return output;
        }


        public void Reset() => _initialized = false;
    }
}