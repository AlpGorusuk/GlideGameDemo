using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame.Statemachine.States
{
    public class IdleState : ICameraState
    {
        private Transform target;
        private Transform cameraTransform;
        private float smoothTime;
        private Vector3 velocity = Vector3.zero;

        public IdleState(Transform target, Transform cameraTransform, float smoothTime)
        {
            this.target = target;
            this.cameraTransform = cameraTransform;
            this.smoothTime = smoothTime;
        }

        public void UpdateState()
        {
            if (target == null) { return; }
            Vector3 targetPosition = target.position;
            Vector3 smoothPosition = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, smoothTime);
            cameraTransform.position = smoothPosition;
        }
    }
}