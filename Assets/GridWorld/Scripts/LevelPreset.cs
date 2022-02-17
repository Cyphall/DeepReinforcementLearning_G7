using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GridWorld
{
    public class LevelPreset : MonoBehaviour
    {

        public TileType[,] Grid = { { TileType.Wall , TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}, 
                                            { TileType.Wall ,TileType.Ground, TileType.Ground, TileType.Ground, TileType.Goal, TileType.Wall},
                                            { TileType.Wall ,TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall},
                                            { TileType.Wall ,TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Wall},
                                            { TileType.Wall ,TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                                            { TileType.Wall ,TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}};
        private void Start()
        {
            int width = Grid.GetLength(0);
            int height = Grid.GetLength(1);
            
            Camera.main.transform.position = new Vector3(width / 2, 5, height / 2);
        }

    }
}
