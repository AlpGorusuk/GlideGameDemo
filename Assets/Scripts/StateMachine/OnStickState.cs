using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnStickState : GameState
    {
        StickController stickController;
        PlayerController playerController;
        public OnStickState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
        {

        }
        public override void Enter()
        {
            base.Enter();
            stickController = gameManager.stickController;
            playerController = gameManager.playerController;

            // playerController.SetPlayerParent(stickController.LaunchPoint);
            // playerController.Position = stickController.LaunchPoint.position;

            CameraController.Instance.SetCameraControllerIdleState(stickController.CameraFollowTransform);
            stickController.ReleaseCallback += x => { stateMachine.ChangeState(GameManager.Instance.onFlyState); };
            stickController.ReleaseCallback += playerController.HandleThrowCallback;

            // stickController.ActivateStickCallback?.Invoke(true);
        }
        public override void Exit()
        {
            base.Exit();
            // stickController.ActivateStickCallback?.Invoke(false);
            stickController.ReleaseCallback -= x => { stateMachine.ChangeState(GameManager.Instance.onFlyState); };
            stickController.ReleaseCallback -= playerController.HandleThrowCallback;
        }
    }
}
