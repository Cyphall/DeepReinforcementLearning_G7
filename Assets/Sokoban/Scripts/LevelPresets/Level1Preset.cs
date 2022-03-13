using UnityEngine;

namespace Sokoban
{
    public class Level1Preset : ALevelPreset
    {
        public Level1Preset()
        {
            Grid = new TileType[,]{
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Button, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
            };

            StartPosition = new Vector2Int(7, 2);

            CrateStartPosition = new[]
                { new Vector2Int(5, 7) };
        }
    }
}