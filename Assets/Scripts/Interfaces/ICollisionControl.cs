using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface ICollisionControl
    {
        void HandleCollision(Collision collision);
    }

}
