using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Animations
{
    public class BendAnimationCommand : IAnimationCommand
    {
        private Animator animator;

        public BendAnimationCommand(Animator anim)
        {
            animator = anim;
        }

        public void Execute(int layer = 0, float normalizedTime = 0)
        {
            animator.Play("Bend", layer, normalizedTime);
        }
    }

    public class ReleaseAnimationCommand : IAnimationCommand
    {
        private Animator animator;

        public ReleaseAnimationCommand(Animator anim)
        {
            animator = anim;
        }

        public void Execute(int layer = 0, float normalizedTime = 0)
        {
            animator.Play("Release", layer, normalizedTime);
        }
    }
}
