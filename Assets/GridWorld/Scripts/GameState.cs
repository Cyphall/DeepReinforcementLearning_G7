using Common.Core;
using Common.Enumeration;
using UnityEngine;

namespace GridWorld
{
    public class GameState : IGameState<GameState>
    {
        public TileType[,] Grid;
        public Vector2Int AgentPos;
        
        public override GameState Copy()
        {
            return new GameState
            {
                Grid = (TileType[,])Grid.Clone(),
                AgentPos = AgentPos
            };
        }

        public override GameStatus Status => this.Grid[this.AgentPos.x, this.AgentPos.y] switch
        {
            TileType.Hole => GameStatus.Lose,
            TileType.Goal => GameStatus.Win,
            _ => GameStatus.Playing
        };
    }
}