using Common.Core;
using Common.Enumeration;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.Game
{
    public class GameState : IGameState<GameState>
    {
        public TileType[,] Grid;
        public Vector2Int AgentPos;
        public Vector2Int[] CratePos;
        public int CurrentIteration = 0;
        public int NbMaxIteration = 10000;

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

        public GameState Copy()
        {
            return new GameState
            {
                Grid = Grid,
                AgentPos = AgentPos,
                CratePos = (Vector2Int[])CratePos.Clone(),
                CurrentIteration = CurrentIteration,
                NbMaxIteration = NbMaxIteration,
            };
        }

        public GameStatus Status
        {
            get
            {
                if (this.CurrentIteration == this.NbMaxIteration)
                    return GameStatus.Lose;
                if (this.VerifyWin())
                    return GameStatus.Win;
                return GameStatus.Playing;
            }
        }

        private Vector2Int[] GetAllButtonsPos()
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

        private bool VerifyWin()
        {
            Vector2Int[] buttonsPos = this.GetAllButtonsPos();
            int nbValidButtons = 0;
            foreach (var buttonPos in buttonsPos)
            {
                foreach (var cratePos in this.CratePos)
                {
                    if (cratePos == buttonPos)
                        nbValidButtons++;
                }
            }

            if (nbValidButtons == buttonsPos.Length)
                return true;
            return false;
        }

        public bool Equals(GameState other)
        {
            if (other == null ||
                this.CratePos.Length != other.CratePos.Length ||
                this.AgentPos != other.AgentPos)
                return false;

            for (int i = 0; i < this.CratePos.Length; ++i)
            {
                if (this.CratePos[i] != other.CratePos[i])
                    return false;
            }

            return true;
        }
    }
}