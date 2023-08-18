using GlideGame.Interfaces;
using GlideGame.Managers;
using GlideGame.Statemachine.States;
using UnityEngine;

namespace GlideGame.Controllers
{
    public class Jumper : MonoBehaviour
    {
        private ICollisionControl jumpControl;
        private GameManager gameManager;
        private void Start()
        {
            jumpControl = GetComponent<ICollisionControl>();
            gameManager = GameManager.Instance;
        }

        private void OnCollisionEnter(Collision collision)
        {
            State gameState = gameManager.stateMachine.CurrentState;
            if (gameState is OnFailState) return;
            jumpControl?.HandleCollision(collision);
        }
    }
}
