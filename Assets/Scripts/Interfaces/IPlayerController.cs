using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IPlayerController
    {
        public Rigidbody RigidBody { get; }
        public bool IsPlaying { get; set; }
        public void InitPlayer();
    }
}
