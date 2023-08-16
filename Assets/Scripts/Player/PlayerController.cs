using System;
using GlideGame.Interfaces;
using GlideGame.ScriptableObjects;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Singleton<PlayerController>, IPlayerController, ICameraFollow, IAnimationController
    {
        [SerializeField] private PlayerSetting playerSetting;
        //
        private Animator animator;
        public Animator Animator
        {
            get
            {
                if (animator == null)
                    animator = GetComponent<Animator>();
                return animator;
            }
        }

        private Rigidbody _rigidbody;
        public Rigidbody RigidBody
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody>();
                return _rigidbody;
            }
        }
        public Vector3 CameraOffset => playerSetting.cameraOffset;
        public Quaternion CameraRotation => playerSetting.cameraRotation;
        //
        public AnimationClip AnimationClip => throw new NotImplementedException();
        public float AnimationTime => throw new NotImplementedException();
        //
        public bool isPlaying { get; set; }
        public bool isHandleRocketEnable { get; set; }
        private Vector3 dragStartPosition;
        private Vector3 dragDelta;
        //
        public Action<float> ThrowPlayerCallback;
        private void Start()
        {
            SetRbIsKinematic(true);
            ThrowPlayerCallback += HandleThrowCallback;
            ThrowPlayerCallback += x => { isPlaying = true; };
        }

        public void InitPlayer()
        {
            SetRbIsKinematic(false);
            SetPlayerParent();
        }
        private void OnDestroy()
        {
            ThrowPlayerCallback -= HandleThrowCallback;
            ThrowPlayerCallback -= x => { isPlaying = true; };
        }

        private void Update()
        {
            if (!isPlaying) return;
            HandleRocket();
            if (!isHandleRocketEnable) { HandleRotation(); }

        }

        private void FixedUpdate()
        {
            if (!isPlaying) return;

            HandleRocketPhysics();
        }

        private void SetPlayerParent()
        {
            Transform targetTransform = GameplayController.Instance.LevelTransform;
            transform.SetParent(targetTransform);
        }
        private void SetRbIsKinematic(bool isKinematic)
        {
            RigidBody.isKinematic = isKinematic;
        }
        private void HandleThrowCallback(float speed)
        {
            float radianAngle = playerSetting.throwAngle * Mathf.Deg2Rad;
            float xVel = speed * Mathf.Cos(radianAngle);
            float yVel = speed * Mathf.Sin(radianAngle);

            Vector3 throwSpeed = new Vector3(0, yVel, xVel);
            RigidBody.AddForce(throwSpeed, ForceMode.VelocityChange);
            Debug.Log("Thrown!");
        }

        private void HandleRotation()
        {
            Quaternion rotation = Quaternion.Euler(Vector3.right * playerSetting.rotationSpeed * Time.deltaTime);
            transform.rotation *= rotation;
        }

        private void HandleRocket()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragStartPosition = Input.mousePosition;
                isHandleRocketEnable = true;
            }

            if (isHandleRocketEnable)
            {
                Vector3 dragCurrentPosition = Input.mousePosition;
                dragDelta = dragCurrentPosition - dragStartPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isHandleRocketEnable = false;
            }
        }

        private void HandleRocketPhysics()
        {
            Vector3 targetPosition = transform.position + new Vector3(dragDelta.x, 0, 0) * 1f * Time.deltaTime;
            transform.position = targetPosition;
        }
    }
}
