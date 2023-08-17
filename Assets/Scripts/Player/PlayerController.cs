using System;
using GlideGame.Animations;
using GlideGame.Interfaces;
using GlideGame.Managers;
using GlideGame.ScriptableObjects;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Singleton<PlayerController>, IPlayerController, ICameraFollow
    {
        [SerializeField] private PlayerSetting playerSetting;
        //
        private Animator animator;
        public Animator Animator
        {
            get
            {
                if (animator == null)
                    animator = GetComponentInChildren<Animator>();
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
        //
        public bool IsPlaying { get; set; }
        public bool IsHandleRocketEnable { get; set; }
        [SerializeField] private GameObject model;
        public GameObject Model => model;

        private Vector3 dragStartPosition;
        private Vector3 dragDelta;
        //
        public Action<float> HandleThrowCallback;
        //Managers
        private readonly AnimationManager animationManager = new();
        //Magic numbs
        private const float rotationAmountCoeff = -1f;
        private void Start()
        {
            SetRbIsKinematic(true);

            HandleThrowCallback += HandleThrow;
            HandleThrowCallback += x => { IsPlaying = true; };

            animationManager.SetCommand(new IdleAnimationCommand(Animator));
            animationManager.ExecuteCommand();
        }

        public void InitPlayer()
        {
            SetRbIsKinematic(false);
            SetPlayerParent();
        }
        private void OnDestroy()
        {
            HandleThrowCallback -= HandleThrow;
            HandleThrowCallback -= x => { IsPlaying = true; };
        }

        private void Update()
        {
            if (!IsPlaying) return;
            HandleInput();
        }
        private void FixedUpdate()
        {
            if (!IsPlaying) return;
            if (!IsHandleRocketEnable) { HandleRotation(); return; }
            HandleRocket();
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
            float radianAngle = playerSetting.throwAngle * Mathf.Deg2Rad;
            float xVel = speed * Mathf.Cos(radianAngle);
            float yVel = speed * Mathf.Sin(radianAngle);

            Vector3 throwSpeed = new(0, yVel, xVel);
            RigidBody.AddForce(throwSpeed, ForceMode.Impulse);
            Debug.Log("Thrown!");
        }

        private void HandleRotation()
        {
            float rotationAmount = playerSetting.rotationSpeed * Time.fixedTime;
            Quaternion targetRotation = Quaternion.Euler(rotationAmount, 0, 0);
            Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, targetRotation, playerSetting.rotationLerpSpeed * Time.fixedTime);
        }

        private void HandleRocket()
        {
            // Drag
            float forceAmount = dragDelta.x * playerSetting.dragOffset * Time.deltaTime;

            // //Gliding
            Vector3 currentVelocity = RigidBody.velocity;
            Vector3 newVelocity = new(currentVelocity.x,
                                              currentVelocity.y * playerSetting.glideOffset,
                                              currentVelocity.z);
            RigidBody.velocity = newVelocity;

            // Rotate the character's transform around its local X-axis
            float rotationAmount = forceAmount * playerSetting.rotationMultiplier * rotationAmountCoeff;
            float clampedRotationAmount = Mathf.Clamp(rotationAmount, playerSetting.minRotationAmount, playerSetting.maxRotationAmount);
            Quaternion deltaRotation = Quaternion.Euler(playerSetting.glideRotationOffset, 0, 0);

            transform.localRotation = Quaternion.Euler(0, 0, clampedRotationAmount);
            Model.transform.localRotation = deltaRotation;
        }
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragStartPosition = Input.mousePosition;
                IsHandleRocketEnable = true;
                animationManager.SetCommand(new RocketOpenedAnimationCommand(Animator));
                animationManager.ExecuteCommand();
            }

            if (IsHandleRocketEnable)
            {
                Vector3 dragCurrentPosition = Input.mousePosition;
                dragDelta = dragCurrentPosition - dragStartPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                IsHandleRocketEnable = false;
                dragDelta = Vector3.zero;
                animationManager.SetCommand(new RocketClosedAnimationCommand(Animator));
                animationManager.ExecuteCommand();
            }
        }
    }
}
