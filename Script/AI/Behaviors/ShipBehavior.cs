using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class ShipBehavior : Behavior
    {    
        
        protected override void InitializeStateMachine(){
            var states = new Dictionary<System.Type, State>(){
                { typeof(ShipStandbyState), new ShipStandbyState(this.gameObject) },
                { typeof(ShipTrackingState), new ShipTrackingState(this.gameObject) },
                { typeof(ShipFiringState), new ShipFiringState(this.gameObject) },
                { typeof(ShipEvadingState), new ShipEvadingState(this.gameObject) },
                { typeof(ShipCirclingState), new ShipCirclingState(this.gameObject) }
            };
            stateMachine.SetStates(states);
        }
    }
}