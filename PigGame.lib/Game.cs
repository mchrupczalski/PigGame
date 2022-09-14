using System.Collections.Generic;
using PigGame.lib.Factories;
using PigGame.lib.Models;

namespace PigGame.lib
{
    public class Game
    {
        public GameEngine Engine { get; }
        private readonly GameScoreModelFactory _gameScoreModelFactory;
        private readonly List<PlayerModel> _players = new List<PlayerModel>();

        public Game(GameEngine gameEngine, GameScoreModelFactory gameScoreModelFactory)
        {
            Engine = gameEngine;
            _gameScoreModelFactory = gameScoreModelFactory;
        }
        
        public void AddPlayer(string name) => _players.Add(new PlayerModel(name, _gameScoreModelFactory.CreateGameScoreModel()));

        public void StartGame()
        {
            Engine.StartGame(_players);
        }

        public void RestartGame()
        {
            _players.Clear();
        }
    }
}