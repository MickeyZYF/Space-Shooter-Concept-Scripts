using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public class PlayerButterflyMovement : PlayerShipMovement
    {
        
        public void Cruise(Rigidbody2D rb, Vector2 movement){
            rb.AddForce(movement * normalSpeed);
        }

        public void Accelerate(Rigidbody2D rb, Vector2 movement){
            rb.AddForce(movement * fastSpeed);
        }
        
        public void Decelerate(Rigidbody2D rb, Vector2 movement){
            rb.AddForce(movement * slowSpeed);
        }

        public void Boost(Rigidbody2D rb, Vector2 movement){
            rb.AddForce(movement * boostSpeed, ForceMode2D.Impulse);
        }
    }
}