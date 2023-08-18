using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using GlideGame.Managers;
using GlideGame.Statemachine.States;
using UnityEngine;

namespace GlideGame
{
    public class JumpControl : MonoBehaviour, ICollisionControl
    {
        [SerializeField] private float upwardForce = 5.0f; // Yukarıya uygulanacak kuvvet miktarı
        public void HandleCollision(Collision collision)
        {
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (otherRigidbody != null)
            {
                otherRigidbody.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
            }
        }
    }
}
