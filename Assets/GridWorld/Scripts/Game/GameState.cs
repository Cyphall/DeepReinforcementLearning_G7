using Common.Core;
using Common.Enumeration;
using UnityEngine;

namespace GridWorld.Game
{
    public class GameState : IGameState<GameState>
    {
        public TileType[,] Grid;
        public Vector2Int AgentPos;

        public GameState Copy()
        {
            return new GameState
            {
                Grid = Grid,
                AgentPos = AgentPos,
            };
        }

        public GameStatus Status => this.Grid[this.AgentPos.x, this.AgentPos.y] switch
        {
            TileType.Hole => GameStatus.Lose,
            TileType.Goal => GameStatus.Win,
            _ => GameStatus.Playing
        };

        public bool Equals(GameState other) => other != null && this.AgentPos == other.AgentPos;
    }
}