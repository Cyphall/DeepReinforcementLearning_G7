using UnityEngine;

namespace GridWorld
{
	public class Level2Preset : ALevelPreset
	{
		public Level2Preset()
		{
			Grid = new TileType[,]{
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall}, 
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground,   TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Wall, TileType.Hole, TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Ground,   TileType.Wall,   TileType.Ground,   TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Wall},
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Ground,   TileType.Wall,   TileType.Wall},
				{ TileType.Wall, TileType.Goal,   TileType.Ground,   TileType.Ground,   TileType.Wall,   TileType.Wall},
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall}
			};

			StartPosition = new Vector2Int(1, 1);
		}
	}
}