using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnStartState : PlayerState
    {
        public OnStartState(StateMachine stateMachine, PlayerController playerController) : base(stateMachine, playerController)
        {
        }
        public override void Enter()
        {
            base.Enter();
            playerController.SetRbIsKinematic(true);
            playerController.InitialRotation = playerController.transform.rotation;
            playerController.IdleAnimCommand();
            playerController.HandleThrowCallback += playerController.HandleThrow;
            playerController.HandleThrowCallback += x => { playerController.IsPlaying = true; };
        }
    }
}