using Common.Core;

namespace GridWorld
{
    public class MoveDown : AGameAction<GameState>
    {   
        public override GameState Apply(GameState state)
        {
            if (state.Grid[state.AgentPos.x, state.AgentPos.y - 1] == TileType.Wall)
            {
                return state;
            }
            
            state.AgentPos.y -= 1;
            return state;
        }
    }
}