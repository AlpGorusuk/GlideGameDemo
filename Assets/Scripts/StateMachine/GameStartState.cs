using System;
using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using GlideGame.UI.Screens;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class GameStartState : GameState
    {
        StickController stickController;
        Action StartGameCallback;
        public GameStartState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            stickController = gameManager.stickController;
            StartGameCallback += stickController.StartCallback;
        }
        public override void Update()
        {
            base.Update();
            StartGame();
        }
        public override void Exit()
        {
            base.Exit();
            StartGameCallback?.Invoke();
            StartGameCallback -= stickController.StartCallback;
        }

        private void StartGame()
        {
            if (Input.GetMouseButtonDown(0))
            {
                stateMachine.ChangeState(gameManager.onStickState);
            }
        }
    }
}
