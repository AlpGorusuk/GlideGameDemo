using System;
using GlideGame.Animations;
using GlideGame.Interfaces;
using GlideGame.Managers;
using GlideGame.ScriptableObjects;
using GlideGame.Statemachine;
using GlideGame.Statemachine.States;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Singleton<PlayerController>, IPlayerController, ICameraFollow
    {
        [Header("Settings")]
        [SerializeField] private PlayerSetting playerSetting;

        [Header("References")]
        [SerializeField] private Animator animator;
        public Animator Animator => animator;
        [SerializeField] private Collider _collider;
        public Collider Collider => _collider;
        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody RigidBody => _rigidbody;
        [SerializeField] private Transform cameraFollowTransform;
        public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }
        public Transform CameraFollowTransform => cameraFollowTransform;
        public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

        [Header("Glide Settings")]
        private float targetRotation = 0f;
        private bool isGliding = false;
        private bool isDragging = false;

        [Header("State")]
        private bool isPlaying = false;
        public bool IsPlaying { get { return isPlaying; } set { isPlaying = value; } }

        [Header("Initial Rotation")]
        private Quaternion initialRotation;
        public Quaternion InitialRotation { get { return initialRotation; } set { initialRotation = value; } }

        [Header("Drag")]
        private Vector3 dragStartPosition;
        private Vector3 dragDelta;

        [Header("Model")]
        [SerializeField] private GameObject model;
        public GameObject Model { get { return model; } set { model = value; } }

        [Header("Callbacks")]
        public Action<float> HandleThrowCallback;
        public Action<Transform> StartCallback => InitPlayer;
        //Managers
        private readonly AnimationManager animationManager = new();
        //States
        public StateMachine stateMachine;
        public OnStartState onStartState;
        public OnPlayState onPlayState;
        public OnLoseState onLoseState;
        //
        Quaternion initQuaternion = Quaternion.Euler(0, 0, 0);
        private void InitPlayer(Transform target)
        {
            SetPlayerParent(target);
            ResetPlayer();
        }
        private void Start()
        {
            //state
            stateMachine = new StateMachine();
            onStartState = new OnStartState(stateMachine, this);
            onPlayState = new OnPlayState(stateMachine, this);
            onLoseState = new OnLoseState(stateMachine, this);
            stateMachine.Initialize(onStartState);
            //
            HandleThrowCallback += x =>
            {
                IsPlaying = true;
                HandleThrow(x);
            };
        }

        private void OnDestroy()
        {
            HandleThrowCallback -= x =>
                        {
                            IsPlaying = true;
                            HandleThrow(x);
                        };
        }

        private void Update()
        {
            if (!IsPlaying)
                return;

            HandleInput();

            if (!isGliding)
                HandleRotation();
            else if (isDragging)
                HandleGlideDrag();

            if (Input.GetMouseButtonUp(0) && isGliding)
                StopGlide();
        }
        private void FixedUpdate()
        {
            if (!IsPlaying)
                return;

            if (isGliding)
            {
                Glide();
                RotateCharacter();
            }
        }
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGlide();
                dragStartPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                HandleDrag();
            }
        }
        private void HandleDrag()
        {
            if (isDragging)
            {
                Vector3 dragCurrentPosition = Input.mousePosition;
                dragDelta = dragCurrentPosition - dragStartPosition;
                AdjustGlideDirection(dragDelta.x);
                HandleModelRotationOnGlide();
                HandleModelRotate();
            }
            else
            {
                isDragging = true;
            }
        }
        private void HandleGlideDrag()
        {
            Vector3 dragCurrentPosition = Input.mousePosition;
            dragDelta = dragCurrentPosition - dragStartPosition;
            AdjustGlideDirection(dragDelta.x);
            HandleModelRotationOnGlide();
            HandleModelRotate();
        }
        //Glide
        private void StartGlide()
        {
            isGliding = true;
            RocketOpenedAnimCommand();
        }
        private void StopGlide()
        {
            isGliding = false;
            isDragging = false;
            RocketClosedAnimCommand();
        }
        private void Glide()
        {
            Vector3 glideVelocity = transform.forward * playerSetting.glideSpeed;
            RigidBody.velocity = glideVelocity;
        }
        private void RotateCharacter()
        {
            float rotationYAmount = targetRotation * playerSetting.glidingRotationSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationYAmount, 0);
            RigidBody.MoveRotation(RigidBody.rotation * deltaRotation);
        }
        private void AdjustGlideDirection(float swipeDelta)
        {
            targetRotation += swipeDelta * Time.deltaTime;
            targetRotation = Mathf.Clamp(targetRotation, playerSetting.minRotationAmount, playerSetting.maxRotationAmount); // Limit rotation angle
        }
        //
        public void SetPlayerParent(Transform targetTransform)
        {
            transform.SetParent(targetTransform);
        }
        public void SetRbIsKinematic(bool isKinematic)
        {
            RigidBody.isKinematic = isKinematic;
        }
        public void EnableCollider(bool isEnable)
        {
            Collider.enabled = isEnable;
        }
        //Throw
        public void HandleThrow(float speed)
        {
            float radianAngle = playerSetting.throwAngle * Mathf.Deg2Rad;
            float xVel = speed * Mathf.Cos(radianAngle);
            float yVel = speed * Mathf.Sin(radianAngle);

            Vector3 throwSpeed = new Vector3(0, yVel, xVel);
            RigidBody.AddForce(throwSpeed, ForceMode.Impulse);
            Debug.Log("Thrown!");
        }
        //
        private void HandleRotation()
        {
            Quaternion rotation = Quaternion.Euler(Vector3.right * playerSetting.rotationSpeed * Time.deltaTime);
            Model.transform.localRotation *= rotation;
        }
        private void HandleModelRotationOnGlide()
        {
            Quaternion modelGlideRot = Quaternion.Euler(playerSetting.glidingXPosition, 0, 0);
            Model.transform.localRotation = Quaternion.Lerp(Model.transform.localRotation, modelGlideRot, playerSetting.glidingLerpValue);
        }
        private void HandleModelRotate()
        {
            Model.transform.Rotate(Vector3.up * dragDelta.x * -0.01f);
        }
        //Reset
        private void ResetPlayer()
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = initQuaternion;
            Model.transform.rotation = initQuaternion;
        }
        //Animation Commands
        public void IdleAnimCommand()
        {
            animationManager.CurrentCommand = new IdleAnimationCommand(Animator);
            animationManager.ExecuteCommand();
        }
        public void RocketOpenedAnimCommand()
        {
            animationManager.CurrentCommand = new RocketOpenedAnimationCommand(Animator);
            animationManager.ExecuteCommand();
        }
        public void RocketClosedAnimCommand()
        {
            animationManager.CurrentCommand = new RocketClosedAnimationCommand(Animator);
            animationManager.ExecuteCommand();
        }
        public void UpdateAnimCommandForLoseState()
        {
            var currCommand = animationManager.CurrentCommand;
            if (currCommand.GetType() == typeof(RocketOpenedAnimationCommand))
            {
                RocketClosedAnimCommand();
            }
        }

        //Changer States
        public void ChangeState(PlayerState state)
        {
            stateMachine.ChangeState(state);
        }
    }
}
