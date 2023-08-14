using System.Collections;
using System.Collections.Generic;
using GlideGame.Utils;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class GameplayController : Singleton<GameplayController>
    {
        public Transform LevelTransform { get => transform; }
    }
}
