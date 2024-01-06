namespace Solver
{
    //When search tree is too large this used as a heurisitic to score a position
    public interface IStaticStateScorer<T> : IStateScorer<T> where T : IGameState<T>
    {
        double approximateUtility(T gameState);
    }
}