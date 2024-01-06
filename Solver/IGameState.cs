namespace Solver
{
    public interface IGameState<T> where T : IGameState<T>
    {
        bool equivalent(T other);
        int getHashCode();
        GameEvent<T> getNextEvent();

    }

}