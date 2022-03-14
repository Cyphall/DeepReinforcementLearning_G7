using UnityEngine;

namespace Sokoban
{
    public class Level4Preset : ALevelPreset
    {
        public Level4Preset()
        {
            Grid = new TileType[,]{
                { TileType.Wall, TileType.Wall, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Wall },
                { TileType.Wall, TileType.Button, TileType.Wall},
                { TileType.Wall, TileType.Wall, TileType.Wall},
            };

            StartPosition = new Vector2Int(1, 1);

            CrateStartPosition = new[]
                { new Vector2Int(2, 1) };
        }
    }
}