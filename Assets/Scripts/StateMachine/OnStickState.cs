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
        CameraController cameraController;
        public OnStickState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
        {

        }
        public override void Enter()
        {
            base.Enter();
            stickController = gameManager.stickController;
            playerController = gameManager.playerController;
            cameraController = gameManager.cameraController;

            stickController.ActivateInputCallback(true);
            cameraController.SetCameraControllerIdleState(stickController.CameraFollowTransform);
            stickController.ReleaseCallback += x =>
            {
                stateMachine.ChangeState(gameManager.onFlyState);
                playerController.HandleThrowCallback(x);
            };
        }
        public override void Exit()
        {
            base.Exit();
            stickController.ActivateInputCallback(false);
            stickController.ReleaseCallback -= x =>
            {
                stateMachine.ChangeState(gameManager.onFlyState);
                playerController.HandleThrowCallback(x);
            };
        }
    }
}
