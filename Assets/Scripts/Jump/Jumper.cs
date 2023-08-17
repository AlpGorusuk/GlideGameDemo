using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class Jumper : MonoBehaviour
    {
        private IJumpControl jumpControl;

        private void Start()
        {
            jumpControl = GetComponent<IJumpControl>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            jumpControl?.HandleCollision(collision);
        }
    }
}
