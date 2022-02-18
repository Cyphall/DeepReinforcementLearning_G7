using Common.Core;
using Common.Enumeration;
using UnityEngine;

namespace Sokoban
{
    public class GameState : AGameState<GameState>
    {
        public TileType[,] Grid;
        public Vector2Int AgentPos;
        public Vector2Int[] CratePos;

        public override GameState Copy()
        {
            return new GameState
            {
                Grid = (TileType[,])Grid.Clone(),
                AgentPos = AgentPos,
                CratePos = (Vector2Int[])CratePos.Clone()
            };
        }

        /* TODO check if each crate is on a button */
        public override GameStatus Status => GameStatus.Playing;
    }
}