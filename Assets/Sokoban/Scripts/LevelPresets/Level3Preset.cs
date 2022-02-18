using UnityEngine;

namespace Sokoban
{
	public class Level3Preset : ALevelPreset
	{
		public Level3Preset()
		{
			Grid = new TileType[,]{
				{ TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}, 
				{ TileType.Wall, TileType.Wall, TileType.Wall, TileType.Ground,  TileType.Ground, TileType.Ground, TileType.Wall, TileType.Wall},
				{ TileType.Wall, TileType.Button, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Wall},
				{ TileType.Wall, TileType.Wall, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Button, TileType.Wall, TileType.Wall},
				{ TileType.Wall, TileType.Button, TileType.Wall, TileType.Wall, TileType.Ground, TileType.Ground, TileType.Wall, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Wall, TileType.Ground, TileType.Button, TileType.Ground, TileType.Wall, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Button, TileType.Ground, TileType.Ground, TileType.Button, TileType.Wall},
				{ TileType.Wall, TileType.Ground, TileType.Ground, TileType.Ground, TileType.Button, TileType.Ground, TileType.Ground, TileType.Wall},
				{ TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}, 
			};

			StartPosition = new Vector2Int(2, 2);

			CrateStartPosition = new[]
				{ new Vector2Int(2, 3) , 
					new Vector2Int(3, 4), 
					new Vector2Int(6, 4), 
					new Vector2Int(6, 3), 
					new Vector2Int(6, 5), 
					new Vector2Int(6, 1), 
					new Vector2Int(4, 4)};
		}
	}
}