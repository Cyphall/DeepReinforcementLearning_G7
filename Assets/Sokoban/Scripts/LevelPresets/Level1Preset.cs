using UnityEngine;

namespace Sokoban
{
	public class Level1Preset : ALevelPreset
	{
		public Level1Preset()
		{
			Grid = new TileType[,]{
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall}, 
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Button,   TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Button, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Button, TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall}
			};

			StartPosition = new Vector2Int(1, 1);

			CrateStartPosition = new[]
				{ new Vector2Int(3, 3), new Vector2Int(4, 3), new Vector2Int(4, 4), new Vector2Int(5, 5) };
		}
	}
}