using GlideGame.Managers;

namespace GlideGame.Statemachine.States
{
    public abstract class State
    {
        protected StateMachine stateMachine;
        protected GameManager gameManager;
        protected State(StateMachine stateMachine, GameManager gameManager)
        {
            this.stateMachine = stateMachine;
            this.gameManager = gameManager;
        }

        public virtual void Enter()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Exit()
        {

        }

        protected virtual void DisplayOnUI()
        {

        }
    }
}
