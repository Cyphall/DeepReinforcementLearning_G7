using UnityEngine;

namespace Sokoban
{
    public class Level7Preset : ALevelPreset
    {
        public Level7Preset()
        {
            Grid = new TileType[,]{
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground,  TileType.Ground, TileType.Button, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall , TileType.Wall, TileType.Wall, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground,  TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground,  TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Button, TileType.Ground, TileType.Ground,  TileType.Ground, TileType.Ground, TileType.Wall},
                { TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall},
            };

            StartPosition = new Vector2Int(3, 2);

            CrateStartPosition = new[]
                { new Vector2Int(2, 1), new Vector2Int(4, 4) };
        }
    }
}