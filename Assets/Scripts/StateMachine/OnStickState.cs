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
            CameraController.Instance.SetCameraController(_stickController.transform, _stickController.CameraOffset);
            _stickController.ReleaseCallback += _playerController.ThrowPlayerCallback;
            _stickController.ReleaseCallback += x => { GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.onFlyState); };
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
            _stickController.ReleaseCallback -= _playerController.ThrowPlayerCallback;
        }
    }
}
