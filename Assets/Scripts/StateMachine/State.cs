namespace GlideGame.Statemachine.States
{
    public abstract class State
    {
        protected StateMachine stateMachine;
        protected State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
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
