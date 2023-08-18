using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnFlyState : State
    {
        PlayerController playerController;
        CameraController cameraController;
        public OnFlyState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            playerController = gameManager.playerController;
            cameraController = gameManager.cameraController;

            playerController.InitPlayer();
            cameraController.SetCameraControllerFollowState(playerController.transform, playerController.CameraFollowTransform);
        }
    }
}
