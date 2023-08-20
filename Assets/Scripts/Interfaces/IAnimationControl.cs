using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IAnimationControl
    {
        public Animator Animator { get; }
        public int AnimatorLayer { get; }
        public float DragDeltaConverter { get; }
        public string BendAnimationName { get; }
        public string ReleaseAnimationName { get; }
        public AnimationClip AnimationClip { get; set; }
        public float AnimationTime { get; set; }
    }
}