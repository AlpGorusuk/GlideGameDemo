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

        private CameraStateManager stateManager;
        public override void Awake()
        {
            base.Awake();
            stateManager = new CameraStateManager();
        }

        public void SetCameraController(Transform target, Quaternion desiredRot, Vector3 offset)
        {
            stateManager.SetState(new FollowState(target, transform, desiredRot, offset, smoothTime));
        }

        private void FixedUpdate()
        {
            stateManager.UpdateState();
        }
    }
}
