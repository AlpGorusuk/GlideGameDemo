using System;
using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using GlideGame.Statemachine;
using GlideGame.Statemachine.States;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Singleton<PlayerController>, IPlayerController, ICameraFollow
    {
        //Interfaces
        [SerializeField] private float angle;
        public float Angle { get { return angle; } private set { angle = value; } }
        public FrameInput Input { get; private set; }
        public bool Grounded { get; private set; }
        private Rigidbody rigidBody;
        public Rigidbody RigidBody { get { return rigidBody; } private set { rigidBody = value; } }
        [SerializeField] private Vector3 cameraOffset;
        public Vector3 CameraOffset { get { return cameraOffset; } private set { cameraOffset = value; } }
        [SerializeField] private GameObject playerModel;
        public GameObject PlayerModel { get { return playerModel; } private set { playerModel = value; } }
        //Actions
        public Action<float> ThrowPlayerCallback;
        //States
        public StateMachine stateMachine;
        public OnRotateState onRotateState;
        public OnRocketState onRocketState;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            ThrowPlayerCallback = Throw;

            stateMachine = new StateMachine();
            onRotateState = new OnRotateState(stateMachine);
            onRocketState = new OnRocketState(stateMachine);
        }
        public void InitPlayer()
        {
            SetPlayerParent();
        }

        public void UpdatePlayer()
        {
        }

        public void SetPlayerParent()
        {
            Transform targetTransform = GameplayController.Instance.LevelTransform;
            transform.SetParent(targetTransform);
        }
        private void Throw(float speed)
        {
            float radianAngle = Angle * Mathf.Deg2Rad;

            float xVel = speed * Mathf.Cos(radianAngle);
            float yVel = speed * Mathf.Sin(radianAngle);

            Vector3 throwSpeed = new Vector3(0, yVel, xVel);
            RigidBody.isKinematic = false;
            RigidBody.AddForce(throwSpeed, ForceMode.VelocityChange);
            Debug.Log("Thrown!");
        }

        #region Gather Input
        private void GatherInput()
        {
            Input = new FrameInput
            {

            };
        }
        #endregion
    }
}
