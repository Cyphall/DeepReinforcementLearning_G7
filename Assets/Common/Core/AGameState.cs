namespace Common.Core
{
    public abstract class AGameState<TDerived>
    {
        public abstract TDerived Copy();
    }
}