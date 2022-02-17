using System;
using System.Numerics;
using Common.Core;
using UnityEngine;

namespace GridWorld
{
    public class GameState : AGameState<GameState>
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
    }
}