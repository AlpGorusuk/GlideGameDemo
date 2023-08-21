using GlideGame.Managers;

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
        public virtual void OnDestroy()
        {

        }

        protected virtual void DisplayOnUI()
        {

        }
    }
}
