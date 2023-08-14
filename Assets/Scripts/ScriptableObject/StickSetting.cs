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
        public float maxDragDistance = 5.0f;
        public float minDragDistance = 0.0f;
        public float dragDelay = 0.001f;
    }
}
