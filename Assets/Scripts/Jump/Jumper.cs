using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class Jumper : MonoBehaviour
    {
        private ICollisionControl jumpControl;

        private void Start()
        {
            jumpControl = GetComponent<ICollisionControl>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            jumpControl?.HandleCollision(collision);
        }
    }
}
