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
        [Header("Throw")]
        public float throwAngle;
        [Header("Camera")]
        public Vector3 cameraOffset;
        public Quaternion cameraRotation;
        [Header("Movement")]
        public float dragOffset = 10f;
        // [Range(200f, 400f)]
        public float maxForceAmount = 200f;
        // [Range(50f, 100f)]
        public float minForceAmount = 50f;
    }
}
