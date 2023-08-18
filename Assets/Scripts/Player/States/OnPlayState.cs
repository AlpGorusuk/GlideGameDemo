using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnPlayState : PlayerState
    {
        public OnPlayState(StateMachine stateMachine, PlayerController playerController) : base(stateMachine, playerController)
        {
        }
        public override void Enter()
        {
            base.Enter();
            playerController.SetRbIsKinematic(false);
            playerController.SetPlayerParent();
            playerController.transform.rotation = playerController.InitialRotation;
        }
    }
}
