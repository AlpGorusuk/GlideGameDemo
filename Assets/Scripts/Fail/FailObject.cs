using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame
{
    public class FailObject : MonoBehaviour
    {
        private ICollisionControl failControl;

        private void Start()
        {
            failControl = GetComponent<ICollisionControl>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            failControl?.HandleCollision(collision);
        }
    }
}
