using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridWorld
{
    public enum TileType
    {
        Ground,
        Wall,
        Hole,
        Goal,
    }

    public class Grid
    {
        public int Width => GridTiles.GetLength(0);
        public int Height => GridTiles.GetLength(1);

        public TileType[,] GridTiles;
        
        public TileType this[int x, int y]
        {
            get => GridTiles[x, y];
            set => GridTiles[x, y] = value;
        }

        public Grid(int width, int height)
        {
            GridTiles = new TileType[Width, Height];
        }

    }

}













