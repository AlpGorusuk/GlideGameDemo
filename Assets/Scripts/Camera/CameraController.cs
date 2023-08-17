using System.Collections;
using System.Collections.Generic;
using GlideGame.Statemachine.States;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class CameraController : Singleton<CameraController>
    {
        public float smoothTime = 0.3f;
        public float rotationSpeed = 0.3f;

        private CameraStateManager stateManager;
        public override void Awake()
        {
            base.Awake();
            stateManager = new CameraStateManager();
        }

        public void SetCameraControllerFollowState(Transform target, Vector3 offset)
        {
            stateManager.SetState(new FollowState(target, transform, offset, smoothTime));
        }

        public void SetCameraControllerRotateState(Transform target, Vector3 offset)
        {
            stateManager.SetState(new RotateState(target, rotationSpeed, transform, offset, smoothTime));
        }

        private void FixedUpdate()
        {
            stateManager.UpdateState();
        }
    }

}
