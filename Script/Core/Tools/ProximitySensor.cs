using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Core
{
    public class ProximitySensor : MonoBehaviour
    {
        public GameObject target;
        public float shipAngle; 
        public float relativeAngle;

        [SerializeField] protected bool proximity = false;

        void Start(){
        }

        void Update(){
            // shipAngle = angleNormalizer(this.transform.parent.localEulerAngles.z);
        }

        void OnTriggerStay2D(Collider2D target){ 
            // if (this.target != null){
            //     return;
            // }

            if (target.gameObject.GetComponent<IFFDevice>() == null){
                return;
            }
            if (this.target != null){
                if (Vector2.Distance(this.target.transform.position, this.transform.position) < Vector2.Distance(target.transform.position, this.transform.position)){
                    return;
                }
            }

            this.target = target.gameObject; 
            proximity = true;
            
            // localEulerAngle returns angle in range from 0 to 360, while Atan2 returns angel in range from 180 to -180
            // We use the angleNormalizer functions to make sure both range from 180 to -180
            Vector2 direction = target.transform.position - this.transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shipAngle = angleNormalizer(this.transform.parent.localEulerAngles.z);

            RelativeAngleCalculator(targetAngle, shipAngle); 
        }

        void OnTriggerExit2D(Collider2D target){
            if (target.gameObject != this.target){
                return;
            }

            if (target.gameObject == this.target){
                this.target = null; 
                //shipAngle = 0f;
                //relativeAngle = 0f; 
                proximity = false;
            }
        }

        // Caculates the angle between this ship and target as if the ship has a 0 Z-Rotation in the inspector
        // This would mean that if the relative angle between the two is positive, the target is "above" the ship,
        // if the relative angle between the two is negative, the target is "below" the ship
        void RelativeAngleCalculator(float targetAngle, float shipAngle){
            relativeAngle = angleNormalizer(targetAngle - shipAngle); 
        }

        // Makes angle range from from 180 to -180 (How it appears in the insepctor)
        float angleNormalizer(float angle){
            if (angle > 180f){
                angle = -360f + angle; 
            }
            if (angle < -180f){
                angle = 360f + angle; 
            }
            return angle; 
        }
    }
}