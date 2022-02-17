using UnityEngine;

namespace GridWorld
{
	public class Level1Preset : ALevelPreset
	{
		public Level1Preset()
		{
			Grid = new TileType[,]{
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall}, 
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Goal,   TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall}
			};

			StartPosition = new Vector2Int(1, 1);
		}
	}
}