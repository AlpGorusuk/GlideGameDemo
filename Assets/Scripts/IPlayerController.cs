using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IPlayerController
    {
        public FrameInput Input { get; }
        public bool Grounded { get; }
        public Rigidbody RigidBody { get; }
        public float Angle { get; }
    }
    public struct FrameInput
    {
        public float Horizontal;
        public bool Vertical;
    }
}
