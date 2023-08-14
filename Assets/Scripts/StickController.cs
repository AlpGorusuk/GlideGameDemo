using System;
using System.Collections;
using System.Collections.Generic;
using GlideGame.Extensions;
using GlideGame.Interfaces;
using GlideGame.ScriptableObjects;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class StickController : Singleton<StickController>, IAnimationController, ICameraFollow
    {
        [SerializeField] private StickSetting stickSetting;
        [SerializeField] private Transform launchPoint;
        //Actions
        public Action<float> ReleaseCallback;

        //Magic Numbers
        private const int animatorLayer = -1;
        private const float dragDeltaConverter = -1;
        private const string BendAnimationName = "Armature_Bend_Stick";
        private const string ReleaseAnimationName = "Armature_Release_Stick";

        //Interface
        public Animator Animator { get; private set; }

        public AnimationClip AnimationClip { get; private set; }

        public bool isPlaying { get; private set; }

        public float AnimationTime { get; private set; }
        public Vector3 CameraOffset { get => stickSetting.cameraOffset; }

        private void Start() => Animator = GetComponent<Animator>();

        public void UpdateStick()
        {
            BendStick();
            ReleaseStick();
        }

        private void BendStick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                stickSetting.dragStartPosition = Input.mousePosition;
                isPlaying = true;
                AnimationClip = Animator.GetAnimationClipByName(BendAnimationName);
            }

            if (isPlaying)
            {
                Vector3 dragCurrentPosition = Input.mousePosition;
                Vector3 dragDelta = dragCurrentPosition - stickSetting.dragStartPosition;
                AnimationTime = dragDelta.x * stickSetting.dragDelay * dragDeltaConverter;
                Animator.Play(AnimationClip.name, animatorLayer, Mathf.Clamp01(AnimationTime / AnimationClip.length));
            }
        }

        private void ReleaseStick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 dragEndPosition = Input.mousePosition;
                float dragDistance = Mathf.Clamp(Vector3.Distance(dragEndPosition, stickSetting.dragStartPosition), stickSetting.minDragDistance, stickSetting.maxDragDistance);

                isPlaying = false;
                AnimationClip = Animator.GetAnimationClipByName(ReleaseAnimationName);
                Animator.Play(AnimationClip.name, animatorLayer, 1 - Mathf.Clamp01(AnimationTime / AnimationClip.length));
                ReleaseCallback?.Invoke(dragDistance);
            }
        }
    }
}
