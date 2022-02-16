using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

// ReSharper disable StringLiteralTypo

namespace GridWorld.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public List<TileToPrefab> TileToPrefabs;

        public LevelPreset level;

        private void Start()
        {
            for (int x = 0; x < level.Grid.Width; ++x)
            {
                for (int y = 0; y < level.Grid.Height; ++y)
                {
                    TileType tileType = level.Grid.GridTiles[x, y];
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