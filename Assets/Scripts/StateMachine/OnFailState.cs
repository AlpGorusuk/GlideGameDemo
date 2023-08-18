using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnFailState : State
    {
        CameraController cameraController;
        public OnFailState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            cameraController = gameManager.cameraController;
            cameraController.SetCameraControllerIdleState();
        }
    }
}
