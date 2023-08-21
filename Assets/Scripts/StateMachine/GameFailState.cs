using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using GlideGame.UI.Screens;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class GameFailState : GameState
    {
        CameraController cameraController;
        PlayerController playerController;
        UIController UIController;
        FailScreen failScreen;
        public GameFailState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            cameraController = gameManager.cameraController;
            UIController = gameManager.UIController;
            failScreen = UIController.FailScreen;

            playerController = gameManager.playerController;
            playerController.ChangeState(playerController.onLoseState);

            cameraController.SetCameraControllerIdleState();
            failScreen.Show();

            //Callback
            if (failScreen.FailCallback != null) return;
            failScreen.FailCallback += () =>
            {
                stateMachine.ChangeState(gameManager.onGameStartState);
                playerController.ChangeState(playerController.onStartState);
            };
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            if (failScreen.FailCallback is null) return;
            failScreen.FailCallback -= () =>
            {
                stateMachine.ChangeState(gameManager.onGameStartState);
                playerController.ChangeState(playerController.onStartState);
            };
        }
    }
}
