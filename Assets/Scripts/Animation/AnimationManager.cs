using System.Collections;
using System.Collections.Generic;
using GlideGame.Animations;
using UnityEngine;

namespace GlideGame.Managers
{
    public class AnimationManager
    {
        private IAnimationCommand currentCommand;
        public IAnimationCommand CurrentCommand { get => currentCommand; set => currentCommand = value; }

        public void ExecuteCommand(int layer = 0, float normalizedTime = 0)
        {
            if (currentCommand != null)
            {
                currentCommand.Execute(layer, normalizedTime);
            }
            else
            {
                Debug.LogWarning("Command NOT Found");
            }
        }
    }

}
