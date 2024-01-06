using System.Diagnostics.CodeAnalysis;

namespace Solver
{


    //A single solver maintains its own cache, a new instance should be created whenver a seperate cache should be used (e.g. new scoring function)
    public class Solver<T> where T : IGameState<T>
    {
        private class GameStateComparator : IEqualityComparer<T>
        {


            public bool Equals(T? x, T? y)
            {
                if(x == null ||  y == null)
                {
                    return false;
                }
               return x.equivalent(y);
            }

            public int GetHashCode([DisallowNull] T obj)
            {
                return obj.getHashCode();
            }
        }
        private Dictionary<T, double> cache = new Dictionary<T, double>(new GameStateComparator());
        public Solver(IStateScorer<T> scorer)
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

        public double ScorePosition(T gameState)
        {
            if (cache.TryGetValue(gameState, out double cachedScore))
            {
                return cachedScore;
            }
            double r = noCacheScorePosition(gameState);
            cache.Add(gameState, r);
            return r;
        }


         private double noCacheScorePosition(T gameState)
        {     
            if (scorer.exactScoreKnown(gameState, out double? exactScore))
            {
                return (double)(exactScore!);
            }
            GameEvent<T> nextEvent = gameState.getNextEvent();
            if (nextEvent is RandomEvent<T>)
            {
                double totalweight=0;
                double total = 0;
                foreach ((T, double) nextState in ((RandomEvent<T>)nextEvent).getNextStates())
                {
                    total += nextState.Item2 * ScorePosition(nextState.Item1);
                    totalweight += nextState.Item2;
                }
                return total/totalweight;
            }
            if(nextEvent is PlayerChoice<T>)
            {
                double bestScore = double.NegativeInfinity;
                foreach(T choice in ((PlayerChoice<T>)nextEvent).getChoices())
                {
                    double score = ScorePosition(choice);
                    if(score > bestScore)
                    {
                        bestScore = score;
                    }
                }
                return bestScore;
            }
            else if(nextEvent == null)
            {
                throw new Exception("Game is over, but exact score is not known");
            }
            else
            {
                throw new Exception("Unknown GameEvent type");
            }




        }
    }
}