using GlideGame.Controllers;
using GlideGame.Managers;

namespace GlideGame.Statemachine.States
{
    public class PlayerState : State
    {
        protected PlayerController playerController;

        public PlayerState(StateMachine stateMachine, PlayerController playerController) : base(stateMachine)
        {
            this.playerController = playerController;
        }
    }
}
