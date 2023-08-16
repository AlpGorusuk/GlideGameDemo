using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnFlyState : State
    {
        private PlayerController _playerController;
        public OnFlyState(StateMachine stateMachine, PlayerController playerController) : base(stateMachine)
        {
            _playerController = playerController;
        }
        public override void Enter()
        {
            base.Enter();
            CameraController.Instance.SetCameraController(_playerController.transform, _playerController.CameraRotation, _playerController.CameraOffset);
            _playerController.InitPlayer();
        }
    }
}
