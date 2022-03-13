using Common.Core;
using GridWorld.Game;

namespace GridWorld.Actions
{
    public class Wait : AGameAction<GameState>
    {
        public override GameState Apply(GameState state)
        {
            return state;
        }
    }
}