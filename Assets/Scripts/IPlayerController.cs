using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IPlayerController
    {
        public Vector3 Velocity { get; }
        public float Radius { get; }
        public FrameInput Input { get; }
        public bool JumpingThisFrame { get; }
        public bool LandingThisFrame { get; }
        public Vector3 RawMovement { get; }
        public bool Grounded { get; }
    }
    public struct FrameInput
    {
        public float Horizontal;
        public bool Vertical;
    }
}
