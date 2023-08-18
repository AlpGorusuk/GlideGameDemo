using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using GlideGame.Managers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnStickState : State
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

            stickController.ActivateStickCallback?.Invoke();
            CameraController.Instance.SetCameraControllerIdleState(stickController.CameraFollowTransform);
            stickController.ReleaseCallback += x => { stateMachine.ChangeState(GameManager.Instance.onFlyState); };
            stickController.ReleaseCallback += playerController.HandleThrowCallback;
        }
        public override void Exit()
        {
            base.Exit();
            stickController.DeActivateStickCallback?.Invoke();
            stickController.ReleaseCallback -= x => { stateMachine.ChangeState(GameManager.Instance.onFlyState); };
            stickController.ReleaseCallback -= playerController.HandleThrowCallback;
        }
    }
}
