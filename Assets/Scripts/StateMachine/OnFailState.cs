using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using GlideGame.UI.Screens;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnFailState : GameState
    {
        CameraController cameraController;
        FailScreen failScreen;
        public OnFailState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            cameraController = gameManager.cameraController;

            UIController UIController = gameManager.UIController;
            failScreen = UIController.FailScreen;

            cameraController.SetCameraControllerIdleState();
            failScreen.Show();
        }
    }
}
