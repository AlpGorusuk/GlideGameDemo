using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnLoseState : PlayerState
    {
        public OnLoseState(StateMachine stateMachine, PlayerController playerController) : base(stateMachine, playerController)
        {
        }
        public override void Enter()
        {
            base.Enter();
            playerController.UpdateAnimCommandForLoseState();
            playerController.IsPlaying = false;
        }
    }
}
