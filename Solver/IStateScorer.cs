namespace Solver
{
    public interface IStateScorer<T> where T : IGameState<T>
    {
        bool exactScoreKnown(T gameState, out double? exactUtility);
       
    }
}