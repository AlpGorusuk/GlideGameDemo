using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Extensions
{
    public static class AnimatorExtensions
    {
        public static AnimationClip GetAnimationClipByName(this Animator animator, string clipName)
        {
            RuntimeAnimatorController controller = animator.runtimeAnimatorController;
            if (controller == null)
            {
                Debug.LogError("Animator does not have a RuntimeAnimatorController assigned.");
                return null;
            }

            foreach (AnimationClip clip in controller.animationClips)
            {
                if (clip.name == clipName)
                {
                    return clip;
                }
            }

            Debug.LogWarning($"Animation clip with name '{clipName}' not found.");
            return null;
        }
    }
}
