using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Core
{
    public class MovementHandler : MonoBehaviour
    {
        protected Rigidbody2D rb;
        [SerializeField] protected float rotateSpeed; 
        [SerializeField] protected float slowRotateSpeed; 
        [SerializeField] protected float maxVelocity; 
        [SerializeField] protected float speed; 

        void Awake(){
            rb = this.GetComponent<Rigidbody2D>();
        }

        void FixedUpdate(){
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }

        public void rotate(Quaternion rotation){
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }

        public void slowRotate(Quaternion rotation){
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, slowRotateSpeed * Time.deltaTime);
        }

        public void move(){
            rb.AddForce(this.transform.right * speed);
        }

        public void trackTarget(GameObject target){
            Vector2 direction = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }

        public void slowTrackTarget(GameObject target){
            Vector2 direction = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, slowRotateSpeed * Time.deltaTime);
        }
    }
}