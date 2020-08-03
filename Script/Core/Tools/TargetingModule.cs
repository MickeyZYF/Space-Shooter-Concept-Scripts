using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Core
{
    public class TargetingModule : MonoBehaviour
    {
        public Collider2D firingArc;
        public GameObject target;
        public bool targetLocked = false;
        [SerializeField] protected float lockonTime; 
        [SerializeField] protected float lockonTimer;

        void Start(){
            // The collider is disabled by default
            firingArc = this.GetComponent<Collider2D>();
            firingArc.enabled = false;
        }

        void OnTriggerStay2D(Collider2D target){
            if (target.gameObject != this.target || 
                    targetLocked){
                return;
            }

            if (lockonTimer >= lockonTime){
                targetLocked = true;
            }
            else {
                lockonTimer += Time.deltaTime; 
            }
        }

        void OnTriggerExit2D(Collider2D target){
            if (target.gameObject != this.target){
                return;
            }
            targetLocked = false;
            lockonTimer = 0f;
        }

        void enableFiringArc(){
            firingArc.enabled = true;
        }

        void disableFiringArc(){
            firingArc.enabled = false; 
        }
    }
}