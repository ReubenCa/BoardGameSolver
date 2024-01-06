using Solver;

namespace NumbersGame
{
    
    internal class Program
    {
        
        //You add a number from 1 to 10 to your total and then a random number from 1-10 is added to your total 
        //you win if you reach over 100
        //you want to win as quick as possible
        static void Main(string[] args)
        {
            Solver<NumbersGameState> solver = new Solver<NumbersGameState>(new NumbersGameScorer());
            NumbersGameState gameState = new NumbersGameState(0, true);
            double Utility = solver.ScorePosition(gameState);
            Console.WriteLine("Utility: " + Utility);   
        }


        
    }

    class NumbersGameScorer : IStateScorer<NumbersGameState>
    {
        public bool exactScoreKnown(NumbersGameState gameState, out double? exactUtility)
        {
            const int goal = 500;
            if (gameState.total > goal)
            {
                exactUtility = -gameState.movesTaken;
                return true;
            }
            exactUtility = null;
            return false;
        }
    }
    class NumbersGameState : IGameState<NumbersGameState>
    {
        
        const int maxAdd = 10;

        public readonly int movesTaken;
       
        public readonly int total;
        public  readonly bool playerNext;
        public NumbersGameState(int total, bool playerNext, int movestaken = 0)
        {
            this.total = total;
            this.playerNext = playerNext;
            this.movesTaken = movestaken;
        }

        public bool equivalent(NumbersGameState other)
        {
            return other is NumbersGameState && ((NumbersGameState)other).total == total && ((NumbersGameState)other).playerNext == playerNext;
        }

        public int getHashCode()
        {
            return  (playerNext ? total * 2 + 1 : total * 2);
        }

        public Solver.GameEvent<NumbersGameState> getNextEvent()
        {
            if(playerNext)
            {
               List<NumbersGameState> nextStates =  new List<NumbersGameState>() ;
                for (int i = 1; i <= maxAdd; i++)
                {
                    nextStates.Add(new NumbersGameState(total + i, false, movesTaken +1));
                }
                return new PlayerChoice<NumbersGameState>(nextStates);
            }
            else
            {
                List<(NumbersGameState, double)> nextStates = new List<(NumbersGameState, double)>();
                for (int i = 1; i <= maxAdd; i++)
                {
                    nextStates.Add((new NumbersGameState(total + i, true, movesTaken + 1), 1.0));
                }
                return new RandomEvent<NumbersGameState>(nextStates);
            }
        }
    }

    
}