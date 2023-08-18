using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GlideGame.Utils
{
    public class FPSCounter : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;
        private float deltaTime;

        public void UpdateFPSCounter()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
        }
    }
}
