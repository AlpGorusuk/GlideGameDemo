using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class RotateState : ICameraState
    {
        private Transform target;
        private Transform cameraTransform;
        private Vector3 offset;
        private float smoothTime;
        private float rotationSpeed;
        private Vector3 velocity = Vector3.zero;

        public RotateState(Transform target, float rotationSpeed, Transform cameraTransform, Vector3 offset, float smoothTime)
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

            // Calculate desired rotation to look at the target's position
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - cameraTransform.position);
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

            // Calculate desired position behind the target
            Vector3 desiredPosition = target.position - target.forward * offset.magnitude;
            cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, desiredPosition, ref velocity, smoothTime);
        }

    }
}
