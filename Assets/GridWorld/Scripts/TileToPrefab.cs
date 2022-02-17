using System;
using UnityEngine;

namespace GridWorld.Scripts
{
    [Serializable]
    public struct TileToPrefab 
    {
        public TileType TileType;
        public GameObject Prefab;
    }
}