using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IJumpControl
    {
        void HandleCollision(Collision collision);
    }

}
