using UnityEngine;

namespace Sokoban
{
    public abstract class ALevelPreset : MonoBehaviour
    {
        public TileType[,] Grid { get; protected set; }

        public Vector2Int StartPosition { get; protected set; }

        public Vector2Int[] CrateStartPosition { get; protected set; }

    }
}
