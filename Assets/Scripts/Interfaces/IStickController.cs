using System.Collections;
using System.Collections.Generic;
using GlideGame.ScriptableObjects;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IStickController
    {
        public Transform LaunchPoint { get; }
        public StickSetting StickSetting { get; }
    }
}
