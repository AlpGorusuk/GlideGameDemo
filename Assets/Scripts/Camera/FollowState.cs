using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class FollowState : ICameraState
    {
        private Transform target;
        private Transform cameraTransform;
        private Quaternion desiredRotation;
        private Vector3 offset;
        private float smoothTime;
        private Vector3 velocity = Vector3.zero;

        public FollowState(Transform target, Transform cameraTransform, Quaternion desiredRotation, Vector3 offset, float smoothTime)
        {
            this.target = target;
            this.cameraTransform = cameraTransform;
            this.offset = offset;
            this.smoothTime = smoothTime;
            this.desiredRotation = desiredRotation;
        }

        public void UpdateState()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(cameraTransform.position, desiredPosition, ref velocity, smoothTime);
            cameraTransform.position = smoothedPosition;

            cameraTransform.rotation = desiredRotation;
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
