using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "StickSetting", menuName = "Game/StickSetting", order = 1)]
    public class StickSetting : ScriptableObject
    {
        [Header("Drag")]
        public float dragMultiplier = 0.5f;
        [Range(100f, 400f)]
        public float maxDragDistance = 200f;
        [Range(0f, 100f)]
        public float minDragDistance = 50f;
        public float dragOffset = 0.001f;
    }
}
