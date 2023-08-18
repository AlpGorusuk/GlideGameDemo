using System.Collections;
using System.Collections.Generic;
using GlideGame.UI.Screens;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class UIController : Singleton<UIController>
    {
        //Scenes
        [SerializeField] private FailScreen failScreen;
        public FailScreen FailScreen => failScreen;
        [SerializeField] private FPSCounter fpsCounter;
        public FPSCounter FPSCounter => fpsCounter;
        private void Start()
        {
        }
        private void Update()
        {
            FPSCounter.UpdateFPSCounter();
        }
    }
}
