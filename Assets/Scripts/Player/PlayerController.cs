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
        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody RigidBody => _rigidbody;
        [SerializeField] private Transform cameraFollowTransform;
        public Transform CameraFollowTransform => cameraFollowTransform;

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
        //Managers
        private readonly AnimationManager animationManager = new();
        //States
        public StateMachine stateMachine;
        public OnStartState onStartState;
        public OnPlayState onPlayState;
        public OnLoseState onLoseState;
        private void Start()
        {
            //state
            stateMachine = new StateMachine();
            onStartState = new OnStartState(stateMachine, this);
            onPlayState = new OnPlayState(stateMachine, this);
            onLoseState = new OnLoseState(stateMachine, this);
            //
            stateMachine.Initialize(onStartState);
        }

        private void OnDestroy()
        {
            HandleThrowCallback -= HandleThrow;
            HandleThrowCallback -= x => { IsPlaying = true; };
        }

        private void Update()
        {
            if (!IsPlaying) return;
            if (!isGliding) { HandleRotation(); }
            if (Input.GetMouseButtonDown(0))
            {
                StartGlide();
                dragStartPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
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
            else if (Input.GetMouseButtonUp(0))
            {
                StopGlide();
            }
        }
        private void FixedUpdate()
        {
            if (!isPlaying) return;
            // Update character movement using Rigidbody velocity
            if (!isGliding) { return; }
            Glide();
            // Update character movement and rotation
            RotateCharacter();
        }
        //Glide
        private void StartGlide()
        {
            isGliding = true;
            animationManager.SetCommand(new RocketOpenedAnimationCommand(Animator));
            animationManager.ExecuteCommand();
        }
        private void StopGlide()
        {
            isGliding = false;
            isDragging = false;
            animationManager.SetCommand(new RocketClosedAnimationCommand(Animator));
            animationManager.ExecuteCommand();
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
        public void SetPlayerParent()
        {
            Transform targetTransform = GameplayController.Instance.LevelTransform;
            transform.SetParent(targetTransform);
        }
        public void SetRbIsKinematic(bool isKinematic)
        {
            RigidBody.isKinematic = isKinematic;
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
        //Animation Commands
        public void IdleAnimCommand()
        {
            animationManager.SetCommand(new IdleAnimationCommand(Animator));
            animationManager.ExecuteCommand();
        }
        //Changer States
        public void ChangeState(PlayerState state)
        {
            stateMachine.ChangeState(state);
        }
    }
}
