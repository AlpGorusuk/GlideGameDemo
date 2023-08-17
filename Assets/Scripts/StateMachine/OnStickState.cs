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
        private PlayerController _playerController;
        public OnStickState(StateMachine stateMachine, StickController stickController, PlayerController playerController) : base(stateMachine)
        {
            _stickController = stickController;
            _playerController = playerController;
        }
        public override void Enter()
        {
            base.Enter();
            _stickController.ActivateStickCallback?.Invoke();
            _stickController.ReleaseCallback += x => { stateMachine.ChangeState(GameManager.Instance.onFlyState); };
            _stickController.ReleaseCallback += _playerController.HandleThrowCallback;
        }
        public override void Exit()
        {
            base.Exit();
            _stickController.DeActivateStickCallback?.Invoke();
            _stickController.ReleaseCallback -= x => { stateMachine.ChangeState(GameManager.Instance.onFlyState); };
            _stickController.ReleaseCallback -= _playerController.HandleThrowCallback;
        }
    }
}
