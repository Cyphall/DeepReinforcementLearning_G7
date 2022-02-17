using System;
using System.Numerics;
using Common.Core;
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
                AgentPos = AgentPos
            };
        }
    }
}