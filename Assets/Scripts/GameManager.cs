using GlideGame.Controllers;
using GlideGame.Statemachine;
using GlideGame.Statemachine.States;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public StateMachine stateMachine;
        public OnStickState onStickState;
        public OnFlyState onFlyState;
        private void Start()
        {
            stateMachine = new StateMachine();
            onStickState = new OnStickState(stateMachine, StickController.Instance, PlayerController.Instance);
            onFlyState = new OnFlyState(stateMachine, PlayerController.Instance);

            stateMachine.Initialize(onStickState);
        }
        private void Update()
        {
            stateMachine.Update_Statemachine();
        }
    }
}
