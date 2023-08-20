using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlideGame.Statemachine.States;

namespace GlideGame.Statemachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            Debug.Log("CurrentState" + CurrentState);
            Debug.Log("---------------------------");
            newState.Enter();
        }
        public void Update_Statemachine()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
        public void FixedUpdate_Statemachine()
        {
            if (CurrentState != null)
            {
                CurrentState.FixedUpdate();
            }
        }
    }
}
