using Common.Core;
using GridWorld.Game;

namespace GridWorld.Actions
{
    public class MoveUp : AGameAction<GameState>
    {
        public override GameState Apply(GameState state)
        {
            if (state.Grid[state.AgentPos.x, state.AgentPos.y + 1] == TileType.Wall)
            {
                return state;
            }

            state.AgentPos.y += 1;
            return state;
        }
    }
}