using System.Runtime.CompilerServices;

namespace Solver
{
    //Can Inherit from Random Event in Game Specific Project if more complex logic is needed
    //T is the type of GameState
    public class RandomEvent<T> : GameEvent<T> where T : IGameState<T>
    {
        private List<(T, double)> nextStates;
        public RandomEvent(List<(T,double)> nextStates)
        {
            this.nextStates = nextStates;
        }

        public virtual List<(T, double)> getNextStates()
        {
            return nextStates;
        }


    }
}