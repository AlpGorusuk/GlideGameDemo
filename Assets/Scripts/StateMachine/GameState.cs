using GlideGame.Managers;

namespace GlideGame.Statemachine.States
{
    public class GameState : State
    {
        protected GameManager gameManager;

        public GameState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine)
        {
            this.gameManager = gameManager;
        }
    }
}
