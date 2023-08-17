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
        [SerializeField] private Animator animator;
        public Animator Animator => animator;

        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody RigidBody => _rigidbody;

        public Vector3 CameraOffset => playerSetting.cameraOffset;
        public Quaternion CameraRotation { get; }
        //
        public AnimationClip AnimationClip => throw new NotImplementedException();
        public float AnimationTime => throw new NotImplementedException();
        //
        private float targetRotation = 0f;
        public float glideSpeed = 5f; // Adjust the glide speed as needed
        public float rotationSpeed = 2f; // Adjust the rotation speed as needed
        //
        public bool isGliding { get; set; }
        public bool isDragging { get; set; }
        private bool isPlaying = false;
        public bool IsPlaying { get { return isPlaying; } set { isPlaying = value; } }
        private Quaternion initialRotation;
        public GameObject Model;

        private Vector3 dragStartPosition;
        private Vector3 dragDelta;
        //
        public Action<float> HandleThrowCallback;
        //Managers
        private readonly AnimationManager animationManager = new();
        private void Start()
        {
            SetRbIsKinematic(true);
            HandleThrowCallback += HandleThrow;
            HandleThrowCallback += x => { IsPlaying = true; };
            animationManager.SetCommand(new IdleAnimationCommand(Animator));
            animationManager.ExecuteCommand();
            initialRotation = transform.rotation;
        }

        public void InitPlayer()
        {
            SetRbIsKinematic(false);
            SetPlayerParent();
            transform.rotation = initialRotation;
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
                // CameraController.Instance.SetCameraControllerRotateState(transform, CameraOffset);
            }
            else if (Input.GetMouseButton(0))
            {
                if (isDragging)
                {
                    Vector3 dragCurrentPosition = Input.mousePosition;
                    dragDelta = dragCurrentPosition - dragStartPosition;
                    AdjustGlideDirection(dragDelta.x);
                    HandleModelRotationOnGlide();
                }
                else
                {
                    isDragging = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                // CameraController.Instance.SetCameraControllerFollowState(transform, CameraOffset);
                StopGlide();
            }

            // Update character movement and rotation
            if (isGliding)
            {
                RotateCharacter();
            }
        }
        private void FixedUpdate()
        {
            if (!isPlaying) return;
            // Update character movement using Rigidbody velocity
            if (!isGliding) return;
            Glide();
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
            Vector3 glideVelocity = transform.forward * glideSpeed;
            RigidBody.velocity = new Vector3(glideVelocity.x, RigidBody.velocity.y * playerSetting.glideMultiplier, RigidBody.velocity.z);
        }
        private void RotateCharacter()
        {
            float rotationAmount = targetRotation * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }
        private void AdjustGlideDirection(float swipeDelta)
        {
            targetRotation += swipeDelta * 1f * Time.deltaTime;
            targetRotation = Mathf.Clamp(targetRotation, playerSetting.minRotationAmount, playerSetting.maxRotationAmount); // Limit rotation angle
        }
        //
        private void SetPlayerParent()
        {
            Transform targetTransform = GameplayController.Instance.LevelTransform;
            transform.SetParent(targetTransform);
        }
        private void SetRbIsKinematic(bool isKinematic)
        {
            RigidBody.isKinematic = isKinematic;
        }
        //Throw
        private void HandleThrow(float speed)
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
            Quaternion modelGlideRot = Quaternion.Euler(playerSetting.ModelGlideXRotation, 0, 0);
            Model.transform.localRotation = Quaternion.Slerp(Model.transform.localRotation, modelGlideRot, 0.1f);
        }
    }
}
