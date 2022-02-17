using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

// ReSharper disable StringLiteralTypo

namespace GridWorld
{
    public class LevelManager : MonoBehaviour
    {
        public List<TileToPrefab> TileToPrefabs;

        public LevelPreset level;

        private void Start()
        {
            for (int x = 0; x < level.Grid.GetLength(0); ++x)
            {
                for (int y = 0; y < level.Grid.GetLength(1); ++y)
                {
                    TileType tileType = level.Grid[x, y];
                    GameObject prefab = null;
                    foreach (var tile in TileToPrefabs)
                    {
                        if (tile.TileType == tileType)
                        {
                            Instantiate(tile.Prefab, new Vector3(x, 0, y), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}