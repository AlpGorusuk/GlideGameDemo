using GlideGame.Controllers;
using GlideGame.Statemachine;
using GlideGame.Statemachine.States;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public StateMachine stateMachine;
        public OnStickState onStickState;
        public OnFlyState onFlyState;
        public OnFailState onFailState;
        //Settings
        [HideInInspector] public PlayerController playerController;
        [HideInInspector] public StickController stickController;
        [HideInInspector] public CameraController cameraController;

        private void Start()
        {
            playerController ??= PlayerController.Instance;

            stickController ??= StickController.Instance;

            cameraController ??= CameraController.Instance;

            //States
            stateMachine = new StateMachine();
            // onStickState = new OnStickState(stateMachine, stickController, playerController);
            // onFlyState = new OnFlyState(stateMachine, playerController);
            onFailState = new OnFailState(stateMachine, this);

            stateMachine.Initialize(onStickState);
        }
        private void Update()
        {
            stateMachine.Update_Statemachine();
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate_Statemachine();
        }
    }
}
