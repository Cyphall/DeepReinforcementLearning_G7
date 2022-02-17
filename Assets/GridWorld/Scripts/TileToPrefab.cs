using System;
using UnityEngine;

namespace GridWorld
{
    [Serializable]
    public struct TileToPrefab 
    {
        public TileType TileType;
        public GameObject Prefab;
    }
}