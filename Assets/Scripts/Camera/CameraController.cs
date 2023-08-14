using System.Collections;
using System.Collections.Generic;
using GlideGame.Statemachine.States;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class CameraController : Singleton<CameraController>
    {
        public float smoothSpeed = 0.125f;

        private CameraStateManager stateManager;
        public override void Awake()
        {
            base.Awake();
            stateManager = new CameraStateManager();
        }

        public void SetCameraController(Transform target, Vector3 offset)
        {
            stateManager.SetState(new FollowState(target, transform, offset, smoothSpeed));
        }

        private void LateUpdate()
        {
            stateManager.UpdateState();
        }
    }
}
