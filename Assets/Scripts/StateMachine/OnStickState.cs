using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnStickState : State
    {
        private StickController _stickController;
        public OnStickState(StateMachine stateMachine, StickController stickController) : base(stateMachine)
        {
            _stickController = stickController;
        }
        public override void Enter()
        {
            base.Enter();
            _stickController.ReleaseCallback += () =>
            {
                stateMachine.ChangeState(GameManager.Instance.onFlyState);
            };
        }
        public override void Update()
        {
            base.Update();
            if (_stickController != null)
            {
                _stickController.UpdateStick();
            }
        }
        public override void Exit()
        {
            base.Exit();
            _stickController.ReleaseCallback -= () => stateMachine.ChangeState(GameManager.Instance.onFlyState);
        }
    }
}
