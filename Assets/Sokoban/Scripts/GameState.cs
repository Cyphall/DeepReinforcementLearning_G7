using System;
using System.Collections.Generic;
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
        public int CurrentIteration = 0;
        public int NbMaxIteration = 10000; // <-- peut être a stocker coté jeu plutot? 

        public bool AnyEntity(int x, int y)
        {
            if (AgentPos.x == x && AgentPos.y == y)
            {
                return true;
            }

            foreach (var pos in CratePos)
            {
                if (pos.x == x && pos.y == y)
                {
                    return true;
                }
            }

            return false;
        }

        public override GameState Copy()
        {
            return new GameState
            {
                Grid = (TileType[,])Grid.Clone(),
                AgentPos = AgentPos,
                CratePos = CratePos,
                CurrentIteration = CurrentIteration
            };
        }

        public Vector2Int[] GetAllButtonsPos()
        {
            List<Vector2Int> res = new List<Vector2Int>();
            for (int x = 0; x < Grid.GetLength(0); ++x)
            {
                for (int y = 0; y < Grid.GetLength(1); ++y)
                {
                    if (Grid[x, y] == TileType.Button)
                        res.Add(new Vector2Int(x, y));
                }
            }

            return res.ToArray();
        }
    }
}