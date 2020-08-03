using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class TurretStandbyState : State
    {
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.TargetingModule targeting; 
        private Quaternion originialRotation; 
        
        public TurretStandbyState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.targeting = behavior.targeting;
            originialRotation = gameObject.transform.rotation;
        }
        public override System.Type Tick(){
            if (targeting.target != null){
                Debug.Log("Turn to Tracking");
                return typeof(TurretTrackingState);
            }
            
            movement.rotate(originialRotation);
            return null;
        }
    }
}