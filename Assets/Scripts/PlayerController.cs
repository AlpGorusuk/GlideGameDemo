using System;
using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Singleton<PlayerController>, IPlayerController, ICameraFollow
    {
        [SerializeField] private float angle;
        public float Angle { get { return angle; } private set { angle = value; } }
        public FrameInput Input { get; private set; }
        public bool Grounded { get; private set; }
        private Rigidbody rigidBody;
        public Rigidbody RigidBody { get { return rigidBody; } private set { rigidBody = value; } }
        [SerializeField] private Vector3 cameraOffset;
        public Vector3 CameraOffset { get { return cameraOffset; } private set { cameraOffset = value; } }

        public Action<float> ThrowPlayerCallback;
        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            ThrowPlayerCallback = Throw;
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
            RigidBody.velocity = throwSpeed;
            Debug.Log("enter here");
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
