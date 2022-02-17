using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct GameState : ICloneable
{
    
    
    public object Clone()
    {
        return null;
    }
}

public enum TileType
{
    Ground,
    Wall,
    Hole,
    Goal,
}
public class Grid
{
    public int Width { get; set; } = 5;
    public int Height { get; set; } = 5;

    public TileType[,] GridTiles;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        GridTiles = new TileType[Width, Height];
    }

}
















