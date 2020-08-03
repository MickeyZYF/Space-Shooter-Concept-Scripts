using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class TurretTrackingState : State
    {
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.TargetingModule targeting; 
        public TurretTrackingState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.targeting = behavior.targeting;
        }
        
        public override System.Type Tick(){
            if (targeting.target == null){
                Debug.Log("Turn to Standby");
                return typeof(TurretStandbyState);
            }
            if (targeting.targetLocked){
                Debug.Log("Turn to Firing");
                return typeof(TurretFiringState);
            }

            movement.trackTarget(targeting.target);
            return null;
        }
    }
}