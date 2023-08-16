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
        public Action<float> HandleThrowCallback;
        private void Start()
        {
            SetRbIsKinematic(true);
            HandleThrowCallback += HandleThrow;
            HandleThrowCallback += x => { isPlaying = true; };
        }

        public void InitPlayer()
        {
            SetRbIsKinematic(false);
            SetPlayerParent();
        }
        private void OnDestroy()
        {
            HandleThrowCallback -= HandleThrow;
            HandleThrowCallback -= x => { isPlaying = true; };
        }

        private void Update()
        {
            if (!isPlaying) return;
            HandleRocket();
            if (!isHandleRocketEnable) { HandleRotation(); }

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
        private void HandleThrow(float speed)
        {
            Debug.Log(speed);
            float radianAngle = playerSetting.throwAngle * Mathf.Deg2Rad;
            float xVel = speed * Mathf.Cos(radianAngle);
            float yVel = speed * Mathf.Sin(radianAngle);

            Vector3 throwSpeed = new Vector3(0, yVel, xVel);
            RigidBody.AddForce(throwSpeed, ForceMode.Impulse);
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
                transform.Translate(Vector3.right * Time.deltaTime * dragDelta.x * 20);
            }

            if (Input.GetMouseButtonUp(0))
            {
                isHandleRocketEnable = false;
            }
            dragStartPosition = Input.mousePosition;
        }
    }
}
