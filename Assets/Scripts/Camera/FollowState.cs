using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class FollowState : ICameraState
    {
        private Transform target;
        private Transform cameraPivot;
        private Transform cameraTransform;
        private float smoothTime;
        private Vector3 velocity = Vector3.zero;

        public FollowState(Transform target, Transform cameraPivot, Transform cameraTransform, float smoothTime)
        {
            this.target ??= target;

            this.cameraPivot ??= cameraPivot;

            this.cameraTransform ??= cameraTransform;

            this.smoothTime = smoothTime;
        }

        public void UpdateState()
        {
            if (target == null) { return; }
            Vector3 targetPosition = cameraPivot.position;
            Vector3 smoothPosition = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, smoothTime);
            cameraTransform.position = smoothPosition;

            cameraTransform.LookAt(target);
        }
    }

    public class CameraStateManager
    {
        private ICameraState currentState;

        public void SetState(ICameraState newState)
        {
            currentState = newState;
        }

        public void UpdateState()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
            }
        }
    }

}
