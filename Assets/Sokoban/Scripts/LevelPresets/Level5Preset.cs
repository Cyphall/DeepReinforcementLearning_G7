using UnityEngine;

namespace Sokoban
{
    public class Level5Preset : ALevelPreset
    {
        public Level5Preset()
        {
            Grid = new TileType[,]{
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground , TileType.Wall},
                { TileType.Wall, TileType.Button, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
            };

            StartPosition = new Vector2Int(1, 1);

            CrateStartPosition = new[]
                { new Vector2Int(2, 2) };
        }
    }
}