using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PigGame.lib.Enums;
using PigGame.lib.Models;

namespace PigGame.lib
{
    public class GameEngine
    {
        #region Fields

        public List<PlayerModel> Players { get; private set; } = new List<PlayerModel>();
        public PlayerModel CurrentPlayer { get; private set; }
        public int GameTurnCounter { get; private set; } = 0;
        public string PlayerTurnEndMessage { get; private set; }
        
        private int _currentPlayerNo = 0;

        #endregion

        #region Constructors

        public GameEngine()
        {
        }

        #endregion

        public void StartGame(IEnumerable<PlayerModel> players)
        {
            Players = (players ?? throw new ArgumentNullException(nameof(players))) as List<PlayerModel>;
            
            Debug.Assert(Players != null, nameof(Players) + " != null");
            if (Players.Count() < 2) throw new ArgumentOutOfRangeException(nameof(players));

            CurrentPlayer = Players[0];
        }

        private void NextPlayer()
        {
            _currentPlayerNo++;
                
            if (_currentPlayerNo == Players.Count - 1)
            {
                _currentPlayerNo = 0;
                GameTurnCounter++;
            }

            CurrentPlayer = Players[_currentPlayerNo];
            CurrentPlayer.Game.NextTurn();
            
            PlayerTurnEndMessage = string.Empty;
        }

        public void PlayerMove(PlayerAction action)
        {
            switch (action)
            {
                case PlayerAction.ThrowDice:
                    CurrentPlayer.Game.PlayTurn();
                    break;
                case PlayerAction.Hold:
                    CurrentPlayer.Game.HoldTurn();
                    NextPlayer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}