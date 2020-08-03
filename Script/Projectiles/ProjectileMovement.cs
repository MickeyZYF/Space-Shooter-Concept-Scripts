using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Projectile
{
    public class ProjectileMovement : MonoBehaviour
    {   
        private Rigidbody2D rb;
        [SerializeField] private float speed = 1f; // We always want this value to be higher than the max. velocity of the ship that fired the projectile
        
        void Awake(){
            rb = this.GetComponent<Rigidbody2D>();
        }

        void FixedUpdate(){
            rb.velocity = this.transform.right * speed;
        }
    }
}