using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame
{
    public interface IAnimationController
    {
        public Animator Animator { get; }
        public AnimationClip AnimationClip { get; }
        public float AnimationTime { get; }
    }
}
