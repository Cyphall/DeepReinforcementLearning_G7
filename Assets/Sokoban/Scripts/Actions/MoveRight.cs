using Sokoban.Game;
using UnityEngine;

namespace Sokoban.Actions
{
    public class MoveRight : Move
    {
        public override GameState Apply(GameState state)
        {
            Vector2Int direction = new Vector2Int(1, 0);
            return ApplyMovement(state, direction);
        }
    }
}