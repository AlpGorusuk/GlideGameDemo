using System;
using System.Collections;
using System.Collections.Generic;
using GlideGame.Extensions;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class StickController : Singleton<StickController>
    {
        private Animator animator;
        private AnimationClip animationClip;
        private bool isPlaying = false;
        private Vector3 dragStartPosition;
        private float animationTime;
        [SerializeField] private float dragDelay = 0.001f;
        public Action ReleaseCallback;

        //Magic Numbers
        private const int animatorLayer = -1;
        private const float dragDeltaConverter = -1;
        private const string BendAnimationName = "Armature_Bend_Stick";
        private const string ReleaseAnimationName = "Armature_Release_Stick";

        private void Start() => animator = GetComponent<Animator>();

        public void UpdateStick()
        {
            BendStick();
            ReleaseStick();
        }

        private void BendStick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragStartPosition = Input.mousePosition;
                isPlaying = true;
                animationClip = animator.GetAnimationClipByName(BendAnimationName);
            }

            if (isPlaying)
            {
                Vector3 dragCurrentPosition = Input.mousePosition;
                Vector3 dragDelta = dragCurrentPosition - dragStartPosition;
                animationTime = dragDelta.x * dragDelay * dragDeltaConverter;
                animator.Play(animationClip.name, animatorLayer, Mathf.Clamp01(animationTime / animationClip.length));
            }
        }

        private void ReleaseStick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isPlaying = false;
                animationClip = animator.GetAnimationClipByName(ReleaseAnimationName);
                animator.Play(animationClip.name, animatorLayer, 1 - Mathf.Clamp01(animationTime / animationClip.length));
                ReleaseCallback?.Invoke();
            }
        }
    }
}
