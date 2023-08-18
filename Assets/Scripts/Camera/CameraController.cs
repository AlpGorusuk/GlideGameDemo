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

        public void SetCameraControllerFollowState(Transform target, Transform cameraPivot)
        {
            stateManager.SetState(new FollowState(target, cameraPivot, transform, smoothTime));
        }
        public void SetCameraControllerIdleState(Transform target)
        {
            stateManager.SetState(new IdleState(target, transform, smoothTime));
        }

        private void FixedUpdate()
        {
            stateManager.UpdateState();
        }
    }

}
