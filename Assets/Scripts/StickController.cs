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
    public class StickController : Singleton<StickController>
    {
        [SerializeField] private StickSetting stickSetting;
        [SerializeField] private Transform launchPoint;
        //Actions
        public Action ActivateStickCallback, DeActivateStickCallback;
        public Action<float> ReleaseCallback;
        //
        private bool isStickActive = false;
        private bool isBendEnable = false;
        //Magic Numbers
        private const int animatorLayer = -1;
        private const float dragDeltaConverter = -1;
        private const string BendAnimationName = "Bend";
        private const string ReleaseAnimationName = "Release";
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
        public AnimationClip AnimationClip { get; private set; }
        public float AnimationTime { get; private set; }
        //Managers
        private AnimationManager animationManager = new AnimationManager();

        private void Start()
        {
            ActivateStickCallback = () => { isStickActive = true; };
            DeActivateStickCallback = () => { isStickActive = false; };
            //Anim command
            animationManager.SetCommand(new IdleAnimationCommand(Animator));
            animationManager.ExecuteCommand();
        }

        public void Update()
        {
            if (!isStickActive) { return; }
            BendStick();
            ReleaseStick();
        }

        private void BendStick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                stickSetting.dragStartPosition = Input.mousePosition;
                isBendEnable = true;
                AnimationClip = Animator.GetAnimationClipByName(BendAnimationName);
            }

            if (isBendEnable)
            {
                Vector3 dragCurrentPosition = Input.mousePosition;
                Vector3 dragDelta = dragCurrentPosition - stickSetting.dragStartPosition;

                AnimationTime = dragDelta.x * stickSetting.dragOffset * dragDeltaConverter;
                animationManager.SetCommand(new BendAnimationCommand(Animator));
                float normalizedTime = Mathf.Clamp01(AnimationTime / AnimationClip.length);
                animationManager.ExecuteCommand(animatorLayer, normalizedTime);
            }
        }

        private void ReleaseStick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isBendEnable = false;

                Vector3 dragEndPosition = Input.mousePosition;
                var clampVal = Vector3.Distance(dragEndPosition, stickSetting.dragStartPosition);
                float dragDistance = Mathf.Clamp(clampVal, stickSetting.minDragDistance, stickSetting.maxDragDistance) * stickSetting.dragMultiplier;

                AnimationClip = Animator.GetAnimationClipByName(ReleaseAnimationName);
                animationManager.SetCommand(new ReleaseAnimationCommand(Animator));
                float normalizedTime = 1 - Mathf.Clamp01(AnimationTime / AnimationClip.length);
                animationManager.ExecuteCommand(animatorLayer, normalizedTime);

                ReleaseCallback?.Invoke(dragDistance);
            }
        }
    }
}
