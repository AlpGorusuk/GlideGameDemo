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
        [HideInInspector] public UIController UIController;
        public override void Awake()
        {
            base.Awake();
            stateMachine = new StateMachine();
        }
        private void Start()
        {
            playerController ??= PlayerController.Instance;

            stickController ??= StickController.Instance;

            cameraController ??= CameraController.Instance;

            UIController ??= UIController.Instance;

            //States
            onStickState = new OnStickState(stateMachine, this);
            onFlyState = new OnFlyState(stateMachine, this);
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
