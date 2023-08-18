using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using GlideGame.Managers;
using GlideGame.Statemachine;
using GlideGame.Statemachine.States;
using UnityEngine;

namespace GlideGame
{
    public class FailControl : MonoBehaviour, ICollisionControl
    {
        private GameManager gameManager;
        private StateMachine stateMachine;
        private void Start()
        {
            gameManager = GameManager.Instance;
            stateMachine = gameManager.stateMachine;
        }
        public void HandleCollision(Collision collision)
        {
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (otherRigidbody != null)
            {
                stateMachine.ChangeState(gameManager.onFailState);
            }
        }
    }
}
