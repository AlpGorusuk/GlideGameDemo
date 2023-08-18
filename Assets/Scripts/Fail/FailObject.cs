using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using GlideGame.Managers;
using GlideGame.Statemachine.States;
using UnityEngine;

namespace GlideGame
{
    public class FailObject : MonoBehaviour
    {
        private ICollisionControl failControl;
        private GameManager gameManager;
        private void Start()
        {
            failControl = GetComponent<ICollisionControl>();
            gameManager = GameManager.Instance;
        }
        private void OnCollisionEnter(Collision collision)
        {
            State gameState = gameManager.stateMachine.CurrentState;
            if (gameState is OnFailState) return;
            failControl?.HandleCollision(collision);
        }
    }
}
