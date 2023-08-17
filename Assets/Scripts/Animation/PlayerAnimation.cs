using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Animations
{
    public class IdleAnimationCommand : IAnimationCommand
    {
        private readonly Animator animator;

        public IdleAnimationCommand(Animator anim)
        {
            animator = anim;
        }

        public void Execute(int layer = 0, float normalizedTime = 0)
        {
            animator.Play("Idle", layer, normalizedTime);
        }
    }

    // Walk animasyon komutu
    public class RocketOpenedAnimationCommand : IAnimationCommand
    {
        private readonly Animator animator;

        public RocketOpenedAnimationCommand(Animator anim)
        {
            animator = anim;
        }

        public void Execute(int layer = 0, float normalizedTime = 0)
        {
            animator.Play("RocketOpened", layer, normalizedTime);
        }
    }

    // Jump animasyon komutu
    public class RocketClosedAnimationCommand : IAnimationCommand
    {
        private readonly Animator animator;

        public RocketClosedAnimationCommand(Animator anim)
        {
            animator = anim;
        }

        public void Execute(int layer = 0, float normalizedTime = 0)
        {
            animator.Play("RocketClosed", layer, normalizedTime);
        }
    }
}
