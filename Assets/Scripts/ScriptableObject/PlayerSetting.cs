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
        public float minRotationAmount, maxRotationAmount;
        public float throwAngle;
        public float modelRotateMultiplier = -0.01f;
        [Header("Glide Settings")]
        public float glideSpeed = 30f;
        public float glidingRotationSpeed = 0.5f;
        public float glidingXPosition = 45f;
        public float glidingLerpValue = 0.1f;
    }
}
