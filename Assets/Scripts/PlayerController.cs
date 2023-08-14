using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class PlayerController : Singleton<PlayerController>, IPlayerController
    {
        public Vector3 Velocity { get; private set; }
        public float Radius { get; private set; }
        public FrameInput Input { get; private set; }
        public bool JumpingThisFrame { get; private set; }
        public bool LandingThisFrame { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public bool Grounded { get; private set; }
        public void InitPlayer()
        {
            SetPlayerParent();
        }

        public void UpdatePlayer()
        {

        }

        public void SetPlayerParent()
        {
            Transform targetTransform = GameplayController.Instance.LevelTransform;
            transform.SetParent(targetTransform);
        }

        #region Gather Input

        private void GatherInput()
        {
            Input = new FrameInput
            {

            };
        }

        #endregion
    }
}
