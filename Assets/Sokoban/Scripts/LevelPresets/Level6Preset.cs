using UnityEngine;

namespace Sokoban
{
    public class Level6Preset : ALevelPreset
    {
        public Level6Preset()
        {
            Grid = new TileType[,]{
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground , TileType.Wall},
                { TileType.Wall, TileType.Button, TileType.Ground, TileType.Button, TileType.Wall},
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
            };

            StartPosition = new Vector2Int(1, 1);

            CrateStartPosition = new[]
                { new Vector2Int(2, 2), new Vector2Int(2, 3) };
        }
    }
}