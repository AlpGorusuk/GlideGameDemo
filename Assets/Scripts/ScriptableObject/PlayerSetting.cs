using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerSetting", menuName = "Game/PlayerSetting", order = 1)]
    public class PlayerSetting : ScriptableObject
    {
        [Header("Rotation")]
        public float rotationSpeed;
        public float rotationMultiplier = 5f;
        public float rotationLerpSpeed = 5f;
        public float minRotationAmount, maxRotationAmount;
        public float throwAngle;
        [Header("Camera")]
        public Vector3 cameraOffset;
        [Header("Movement")]
        public float dragOffset = 10f;
        public float glideMultiplier = 0.5f;
        public float ModelGlideXRotation = 45f;
    }
}
