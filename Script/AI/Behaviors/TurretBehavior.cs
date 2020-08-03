using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public class TurretBehavior : Behavior
    {    
        protected override void InitializeStateMachine(){
            var states = new Dictionary<System.Type, State>(){
                { typeof(TurretStandbyState), new TurretStandbyState(this.gameObject) },
                { typeof(TurretTrackingState), new TurretTrackingState(this.gameObject) },
                { typeof(TurretFiringState), new TurretFiringState(this.gameObject) }
            };
            stateMachine.SetStates(states);
        }
    }
}