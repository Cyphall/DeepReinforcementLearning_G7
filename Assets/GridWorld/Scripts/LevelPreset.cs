using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPreset : MonoBehaviour
{
    public Grid Grid;

    private TileType[,] _presetLevelTiles = { { TileType.Wall , TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}, 
                                        { TileType.Wall ,TileType.Ground, TileType.Ground, TileType.Ground, TileType.Goal, TileType.Wall},
                                        { TileType.Wall ,TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall},
                                        { TileType.Wall ,TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Wall},
                                        { TileType.Wall ,TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                                        { TileType.Wall ,TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}};
    private void Start()
    {
        int width = _presetLevelTiles.GetLength(0);
        int height = _presetLevelTiles.GetLength(1);
        
        Grid = new Grid(width,height);
        Camera.main.transform.position = new Vector3(width / 2, 5, height / 2);
        Grid.GridTiles = _presetLevelTiles;
    }

}
