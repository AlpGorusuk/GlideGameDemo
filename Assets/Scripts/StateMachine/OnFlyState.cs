using System.Collections;
using System.Collections.Generic;
using GlideGame.Controllers;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class OnFlyState : State
    {
        public OnFlyState(StateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            PlayerController.Instance.InitPlayer();
        }
        public override void Update()
        {
            base.Update();
            PlayerController.Instance.UpdatePlayer();
        }
    }
}
