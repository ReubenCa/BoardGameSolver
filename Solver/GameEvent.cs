namespace Solver
{
    public abstract class GameEvent<T> where T : IGameState<T>
    {
        
    }

    public class PlayerChoice<T> : GameEvent<T> where T : IGameState<T>
    {
        public PlayerChoice(List<T> choices)
        {
            this.choices = choices;
        }
        private List<T> choices;
        public List<T> getChoices()
        {
            return choices;
        }
    }
}