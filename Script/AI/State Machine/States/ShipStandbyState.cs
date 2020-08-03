using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class ShipStandbyState : State
    {
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.ProximitySensor proximity; 
        private MZYF.Core.TargetingModule targeting;

        public ShipStandbyState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.proximity = behavior.proximity; 
            this.targeting = behavior.targeting;
        }

        public override System.Type Tick(){
            if (proximity.target != null){
                behavior.previousState = typeof(ShipStandbyState);

                if (proximity.target == targeting.target){
                    Debug.Log("Turn to Circling");
                    return typeof(ShipCirclingState);
                }
                Debug.Log("Turn to Evading");
                return typeof(ShipEvadingState);
            }

            if (targeting.target != null){
                Debug.Log("Turn to Tracking");
                return typeof(ShipTrackingState);
            }
            
            movement.move();
            return null;
        }
    }
}