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
        private Vector3 offset;
        private float smoothTime;
        private Vector3 velocity = Vector3.zero;
        private float rotationSpeed;

        public FollowState(Transform target, float rotationSpeed, Transform cameraTransform, Vector3 offset, float smoothTime)
        {
            this.target = target;
            this.cameraTransform = cameraTransform;
            this.offset = offset;
            this.smoothTime = smoothTime;
            this.rotationSpeed = rotationSpeed;
        }

        public void UpdateState()
        {
            if (target == null) { return; }
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - cameraTransform.position);
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

            // Kameranın hedefin arkasından bakmasını sağla ve pozisyon ofsetini uygula
            Vector3 desiredPosition = target.position + offset;
            cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, desiredPosition, ref velocity, smoothTime);
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
