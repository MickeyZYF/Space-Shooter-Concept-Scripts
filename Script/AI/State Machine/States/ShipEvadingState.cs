using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
    {
    // Evasion is a temporary state, should return to the state that switched to evade state when done evading
    // We may want to add a timer to evade state in the future. 
    public class ShipEvadingState : State
    {   
        private MZYF.Core.MovementHandler movement;
        private MZYF.Core.ProximitySensor proximity; 
        private float evademagnitude = 60f; 

        public ShipEvadingState(GameObject gameObject) : base(gameObject.gameObject){
            this.movement = behavior.movement;
            this.proximity = behavior.proximity; 
        }

        public override System.Type Tick(){
            if (proximity.target == null){
                Debug.Log("Turn to Previous State");
                return behavior.previousState;
            }

            // We normalized the angle between the ship and the obstacle in proximity sensor, meaning that if the angle is positve, the obstacle is "above/right", and if the angle is negative, the obstacle is "below/left"
            Quaternion evadeAngle; 
            if (proximity.relativeAngle >= 0f){
                evadeAngle = Quaternion.AngleAxis(proximity.shipAngle - evademagnitude, Vector3.forward);
            }
            else {
                evadeAngle = Quaternion.AngleAxis(proximity.shipAngle + evademagnitude, Vector3.forward);
            }

            movement.rotate(evadeAngle); 
            movement.move();
            return null;
        }
    }
}