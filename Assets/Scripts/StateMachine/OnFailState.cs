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
        PlayerController playerController;
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

            playerController = gameManager.playerController;
            playerController.ChangeState(playerController.onLoseState);

            cameraController.SetCameraControllerIdleState();
            failScreen.Show();
            //Callbacks
            failScreen.FailCallback += () => playerController.ChangeState(playerController.onStartState);
            failScreen.FailCallback += () => stateMachine.ChangeState(gameManager.onStickState);
        }
        public override void Exit()
        {
            failScreen.FailCallback -= () => playerController.ChangeState(playerController.onStartState);
            failScreen.FailCallback -= () => stateMachine.ChangeState(gameManager.onStickState);
            base.Exit();
        }
    }
}
