using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Sokoban
{
    public abstract class ALevelPreset : MonoBehaviour
    {
        public TileType[,] Grid { get; protected set; }
        
        public Vector2Int StartPosition { get; protected set; }
        
        public Vector2Int[] CrateStartPosition { get; protected set; }
        
    }
}
