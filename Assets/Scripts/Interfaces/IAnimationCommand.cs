using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Animations
{
    public interface IAnimationCommand
    {
        void Execute(int layer = 0, float normalizedTime = 0);
    }
}
