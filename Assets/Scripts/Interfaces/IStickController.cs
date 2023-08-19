using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IStickController
    {
        public bool IsPlaying { get; set; }
        public bool IsBendEnable { get; set; }
    }
}
