namespace Solver
{
    public abstract class GameEvent
    {
        
    }

     
    //Can Inherit from Random Event in Game Specific Project if more complex logic is needed
    //T is the type of GameState
    public class RandomEvent : GameEvent
    {

    }

    public interface IGameState
    {
        public bool equivalent(IGameState other);
        public GameEvent getNextEvent();
    }

    //When search tree is too large this used as a heurisitic to score a position
    public interface IStaticStateScorer<T> : IStateScorer<T> where T : IGameState
    {
        double approximateUtility(IGameState gameState);
    }

    public interface IStateScorer<T> where T : IGameState
    {
        bool exactScoreKnown(T gameState, out double exactUtility);
       
    }


    //A single solver maintains its own cache, a new instance should be created whenver a seperate cache should be used (e.g. new scoring function)
    public class Solver<T> where T : IGameState
    {
        Dictionary

        Solver(IStateScorer<T> scorer)
        { 
            this.scorer = scorer;
            if(scorer is IStaticStateScorer<T>)
            {
                staticscorer = (IStaticStateScorer<T>) scorer;
            }
            else
            {
                staticscorer = null;
            }
        }
        
        IStateScorer<T> scorer;
        IStaticStateScorer<T>? staticscorer;

       
        public int chooseBestMove(T gameState)
        {
            throw new NotImplementedException();
        }

        public double ScorePosition(IGameState gameState)
        {
            if(scorer.exactScoreKnown((T)gameState, out double exactScore))
            {
                return (double) exactScore;
            }
         
           
        }
    }
}