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
        [Header("Throw")]
        public float throwAngle;
        [Header("Camera")]
        public Vector3 cameraOffset;
        public Quaternion cameraRotation;
    }
}
