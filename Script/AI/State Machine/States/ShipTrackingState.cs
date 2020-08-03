using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class ShipTrackingState : State
    {
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.ProximitySensor proximity;
        private MZYF.Core.TargetingModule targeting;
        
        public ShipTrackingState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.proximity = behavior.proximity; 
            this.targeting = behavior.targeting;
        }
        
        public override System.Type Tick(){
            if (proximity.target != null){
                behavior.previousState = typeof(ShipTrackingState);
                if (proximity.target == targeting.target){
                    Debug.Log("Turn to Circling");
                    return typeof(ShipCirclingState);
                }
                Debug.Log("Turn to Evading");
                return typeof(ShipEvadingState);
            }

            if (targeting.target == null){
                Debug.Log("Turn to Standby");
                return typeof(ShipStandbyState);
            }

            if (targeting.targetLocked){
                Debug.Log("Turn to Firing");
                return typeof(ShipFiringState);
            }

            movement.slowTrackTarget(behavior.targeting.target);
            movement.move();
            return null;
        }
    }
}