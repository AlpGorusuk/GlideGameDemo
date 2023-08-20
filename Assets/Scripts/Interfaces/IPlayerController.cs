using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IPlayerController
    {
        public Rigidbody RigidBody { get; }
        public Collider Collider { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public bool IsPlaying { get; set; }
    }
}
