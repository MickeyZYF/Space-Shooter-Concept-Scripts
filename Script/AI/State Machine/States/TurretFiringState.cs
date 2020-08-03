using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class TurretFiringState : State
    {
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.TargetingModule targeting; 
        private MZYF.Core.WeaponHandler weapon;

        public TurretFiringState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.targeting = behavior.targeting;
            this.weapon = behavior.primaryWeapon; 
        }

        public override System.Type Tick(){
            if (!targeting.targetLocked){
                Debug.Log("Turn to Tracking");
                return typeof(TurretTrackingState);
            }
            if (targeting.target == null){
                Debug.Log("Turn to Standby");
                return typeof(TurretStandbyState);}



            movement.trackTarget(targeting.target);
            weapon.fire();
            return null;
        }
    }
}