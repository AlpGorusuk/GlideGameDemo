using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnPlayState : PlayerState
    {
        GameplayController gameplayController;
        public OnPlayState(StateMachine stateMachine, PlayerController playerController) : base(stateMachine, playerController)
        {
        }
        public override void Enter()
        {
            base.Enter();
            gameplayController = GameplayController.Instance;
            playerController.SetRbIsKinematic(false);
            playerController.EnableCollider(true);
            playerController.SetPlayerParent(gameplayController.LevelTransform);
            playerController.transform.rotation = playerController.InitialRotation;
        }
    }
}
