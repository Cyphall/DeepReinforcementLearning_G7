using System;
using UnityEngine;

namespace Sokoban
{
    [Serializable]
    public struct TileToPrefab
    {
        public TileType TileType;
        public GameObject Prefab;
    }
}