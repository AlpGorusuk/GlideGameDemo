using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "StickSetting", menuName = "Game/StickSetting", order = 1)]
    public class StickSetting : ScriptableObject
    {
        public Vector3 cameraOffset;
        public Vector3 dragStartPosition;
        [Range(200f, 400f)]
        public float maxDragDistance = 200f;
        [Range(50f, 100f)]
        public float minDragDistance = 50f;
        public float dragDelay = 0.001f;
    }
}
