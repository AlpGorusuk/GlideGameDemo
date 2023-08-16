using GlideGame.Controllers;

namespace GlideGame.Statemachine.States
{
    public abstract class PlayerState : State
    {
        protected PlayerController playerController;
        protected PlayerState(StateMachine stateMachine, PlayerController playerController) : base(stateMachine)
        {
            this.playerController = playerController;
        }
    }
}
