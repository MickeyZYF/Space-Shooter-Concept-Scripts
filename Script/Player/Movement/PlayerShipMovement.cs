using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public class PlayerShipMovement : MonoBehaviour
    {
            [SerializeField] protected float rotateSpeed; 
            [SerializeField] protected float normalSpeed; // Speed of the ship when no keys are pressed (Ship Mode)
            [SerializeField] protected float slowSpeed; // Speed of the ship when the "Accelerate" key is pressed (Ship Mode)
            [SerializeField] protected float fastSpeed; // Speed of the ship when the "Decelerate" key is pressed (Ship Mode)
            [SerializeField] protected float boostSpeed; // Speed of the ship when the "Accelerate" and "Boost" key is pressed (Ship Mode) 

        public void ShipRotation(){
            Vector2 direction = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotateSpeed * 100f * Time.deltaTime);
        }

        public void Cruise(Rigidbody2D rb){
            rb.AddForce(this.transform.right * normalSpeed);
        }
        public void Accelerate(Rigidbody2D rb){
            rb.AddForce(this.transform.right * fastSpeed);
        }
        
        public void Decelerate(Rigidbody2D rb){
            rb.AddForce(this.transform.right * slowSpeed);
        }

        public void Boost(Rigidbody2D rb){
            rb.AddForce(this.transform.right * boostSpeed, ForceMode2D.Impulse);
        }
    }
}