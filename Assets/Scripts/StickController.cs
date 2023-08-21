using System;
using System.Collections;
using System.Collections.Generic;
using GlideGame.Animations;
using GlideGame.Extensions;
using GlideGame.Interfaces;
using GlideGame.Managers;
using GlideGame.ScriptableObjects;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class StickController : Singleton<StickController>, IStickController, ICameraFollow, IAnimationControl
    {
        [SerializeField] private StickSetting stickSetting;
        public StickSetting StickSetting { get { return stickSetting; } set { stickSetting = value; } }
        [SerializeField] private Transform launchPoint;
        public Transform LaunchPoint => launchPoint;
        //Actions
        public Action StartCallback => InitStick;
        public Action<float> ReleaseCallback;
        public Action<bool> ActivateInputCallback => x => { isInputEnable = x; };
        //
        private bool isBendEnable = false;
        private bool isInputEnable = false;
        //Animation Control
        public int AnimatorLayer => animatorLayer;
        public float DragDeltaConverter => dragDeltaConverter;
        public string BendAnimationName => bendAnimationName;
        public string ReleaseAnimationName => releaseAnimationName;
        //Anim data
        private const int animatorLayer = -1;
        private const float dragDeltaConverter = -1;
        private const string bendAnimationName = "Bend";
        private const string releaseAnimationName = "Release";
        public AnimationClip AnimationClip { get; set; }
        public float AnimationTime { get; set; }
        //Drag
        private Vector3 dragStartPosition;
        private Vector3 dragDelta;
        //Animation Control
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
        [SerializeField] private Transform cameraFollowTransform;
        public Transform CameraFollowTransform => cameraFollowTransform;

        //Managers
        private AnimationManager animationManager = new AnimationManager();

        private void InitStick()
        {
            StartBendAnimation();

            animationManager.CurrentCommand = new IdleAnimationCommand(Animator);
            animationManager.ExecuteCommand();
        }
        private void Update()
        {
            if (!isInputEnable) return;
            HandleInput();
        }
        private void HandleInput()
        {
            if (Input.GetMouseButton(0))
            {
                ContinueBendAnimation();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndBendAnimation();
            }
        }
        private void StartBendAnimation()
        {
            dragStartPosition = Input.mousePosition;
            AnimationClip = Animator.GetAnimationClipByName(BendAnimationName);

            isBendEnable = true;
        }
        private void ContinueBendAnimation()
        {
            if (isBendEnable)
            {
                Vector3 dragCurrentPosition = Input.mousePosition;
                dragDelta = dragCurrentPosition - dragStartPosition;
                AnimationTime = dragDelta.x * StickSetting.dragOffset * dragDeltaConverter;
                animationManager.CurrentCommand = new BendAnimationCommand(Animator);
                float normalizedTime = Mathf.Clamp01(AnimationTime / AnimationClip.length);
                animationManager.ExecuteCommand(animatorLayer, normalizedTime);
            }
        }
        private void EndBendAnimation()
        {
            if (Input.GetMouseButtonUp(0) && isBendEnable)
            {
                isBendEnable = false;

                float dragDistance = Mathf.Clamp(Mathf.Abs(dragDelta.x), stickSetting.minDragDistance, stickSetting.maxDragDistance) * stickSetting.dragMultiplier;
                AnimationClip = Animator.GetAnimationClipByName(ReleaseAnimationName);
                animationManager.CurrentCommand = new ReleaseAnimationCommand(Animator);
                float normalizedTime = 1 - Mathf.Clamp01(AnimationTime / AnimationClip.length);
                animationManager.ExecuteCommand(animatorLayer, normalizedTime);
                ReleaseCallback?.Invoke(dragDistance);
            }
        }
    }
}
