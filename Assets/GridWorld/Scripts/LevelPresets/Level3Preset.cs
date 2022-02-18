using UnityEngine;

namespace GridWorld
{
	public class Level3Preset : ALevelPreset
	{
		public Level3Preset()
		{
			Grid = new TileType[,]{
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}, 
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Goal,   TileType.Ground, TileType.Ground, TileType.Hole, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Wall, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall,   TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground, TileType.Wall, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground, TileType.Wall, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground, TileType.Wall, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground,   TileType.Ground, TileType.Hole, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall,   TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}
			};

			StartPosition = new Vector2Int(8, 7);
		}
	}
}