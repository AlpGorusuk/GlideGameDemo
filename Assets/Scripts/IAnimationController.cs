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
        public void PlayAnimator(string name, int layer = 0, float normalizedTime = 0);
    }
}
