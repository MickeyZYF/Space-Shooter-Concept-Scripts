using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
    {
    // The purpose of this state is that when the ship gets too close to their target, they want to turn around, get some distance between them and their targets, and then loop back so they can keep shooting
    public class ShipCirclingState : State
    {
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.ProximitySensor proximity;
        private MZYF.Core.TargetingModule targeting;
        private float circlingMagnitude = 90f; 
        // private GameObject circlingTarget; 
        private float circlingTime = 15f;
        private float circlingTimer; 
        private System.Type storedState;

        public ShipCirclingState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.proximity = behavior.proximity; 
            this.targeting = behavior.targeting;
        }

        public override System.Type Tick(){
            if (proximity.target != targeting.target && 
                    proximity.target != null){
                Debug.Log("Turn to Evading");
                storedState = behavior.previousState;
                behavior.previousState = typeof(ShipCirclingState);
                return typeof(ShipEvadingState);
            }
            
            if (circlingTimer >= circlingTime){
                Debug.Log("Turn to Previous State");
                circlingTimer = 0f; 

                if (storedState == null){
                    return behavior.previousState;
                }

                System.Type previousState = storedState;
                storedState = null; 
                return previousState;
            }

            circlingTimer += Time.deltaTime; 

            Quaternion evadeAngle; 
            if (proximity.relativeAngle >= 0f){
                evadeAngle = Quaternion.AngleAxis(proximity.shipAngle - circlingMagnitude, Vector3.forward);
                movement.rotate(evadeAngle);
            }
            else {
                evadeAngle = Quaternion.AngleAxis(proximity.shipAngle + circlingMagnitude, Vector3.forward);
                movement.rotate(evadeAngle);
            }

            movement.move();
            return null;
        }
    }
}