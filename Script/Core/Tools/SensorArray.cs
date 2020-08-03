using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Core
{
    public class SensorArray : MonoBehaviour
    {
        private IFFDevice targetIFF;
        public TargetingModule targeting; 
        public List<MZYF.Core.IFFDevice.Identification> validTargets = new List<MZYF.Core.IFFDevice.Identification>();

        void Start(){
            targetIFF = this.transform.parent.GetComponent<IFFDevice>();
        }

        // OnTriggerEnter will trigger on the children of gameobjects that have collider, such like the guns on a ship that would not have an IFF
        // Must check if IFF is NUll or not, some objects, like the children of gameobjects, would not have IFF tags
        void OnTriggerEnter2D(Collider2D target){
            if (targeting.target != null){
                return;
            }
     
            if (target.gameObject.GetComponent<IFFDevice>() == null){
                return;
            }
            
            if (validTarget(target.gameObject)){
                targeting.target = target.gameObject;
                targeting.firingArc.enabled = true; 
            }
            else {
                return;
            }
        }

        void OnTriggerExit2D(Collider2D target){
            if (targeting.target == null){
                return;
            }

            if (target.gameObject == targeting.target){
                targeting.target = null;
                targeting.firingArc.enabled = false;
            }
        }

        // This function determines whether the object should be targeted or not
        // The GameObject passed to this function will have an valid IFFDevice attached
        bool validTarget(GameObject target){
            GameObject parent = this.transform.parent.gameObject; 

            // If the faction is friend, then it is not a valid target, thus return false
            if (targetIFF.friendOrFoe(parent, target)){
                return false;
            }

            foreach (MZYF.Core.IFFDevice.Identification identification in validTargets){
                if (targetIFF.checkIdentity(target, identification)){
                    return true;
                }
            }
            return false;
        }
    }
}