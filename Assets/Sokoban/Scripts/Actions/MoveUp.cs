using Sokoban.Game;
using UnityEngine;

namespace Sokoban.Actions
{
    public class MoveUp : Move
    {
        public override GameState Apply(GameState state)
        {
            Vector2Int direction = new Vector2Int(0, 1);

            return ApplyMovement(state, direction);
        }
    }
}