using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class ShipFiringState : State
    {
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.ProximitySensor proximity; 
        private MZYF.Core.TargetingModule targeting; 
        private MZYF.Core.WeaponHandler primaryweapon;
        private MZYF.Core.WeaponHandler secondaryWeapon;

        public ShipFiringState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.proximity = behavior.proximity;
            this.targeting = behavior.targeting;
            this.primaryweapon = behavior.primaryWeapon; 
            this.secondaryWeapon = behavior.secondaryWeapon; 
        }

        public override System.Type Tick(){
            if (proximity.target != null){
                behavior.previousState = typeof(ShipFiringState);

                if (proximity.target == targeting.target){
                    Debug.Log("Turn to Circling");
                    return typeof(ShipCirclingState);
                }
                Debug.Log("Turn to Evading");
                return typeof(ShipEvadingState);
            }

            if (!behavior.targeting.targetLocked){
                Debug.Log("Turn to Tracking");
                return typeof(ShipTrackingState);
            }

            if (behavior.targeting.target == null){
                Debug.Log("Turn to Standby");
                return typeof(ShipStandbyState);
            }

            movement.trackTarget(targeting.target);
            movement.move();
            primaryweapon.fire();
            return null;
        }
    }
}