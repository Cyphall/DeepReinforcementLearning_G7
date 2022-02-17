using Common.Core;

namespace GridWorld
{
    public class MoveRight : AGameAction<GameState>
    {   
        public override GameState Apply(GameState state)
        {
            if (state.Grid[state.AgentPos.x + 1, state.AgentPos.y] == TileType.Wall)
            {
                return state;
            }
            
            state.AgentPos.x += 1;
            return state;
        }
    }
}