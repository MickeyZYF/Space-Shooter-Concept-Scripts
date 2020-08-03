using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    // Behaviors initializes the statemachine for the AI, as well as providing the neccessary information that the states require to function
    // It is essentially the core of the AI 
    public abstract class Behavior : MonoBehaviour
    {    
        public StateMachine stateMachine;
        public MZYF.Core.IFFDevice IFF;
        public MZYF.Core.ProximitySensor proximity; 
        // public MZYF.Core.SensorArray sensor;
        public MZYF.Core.TargetingModule targeting;
        public MZYF.Core.MovementHandler movement; 
        public MZYF.Core.WeaponHandler primaryWeapon;
        public MZYF.Core.WeaponHandler secondaryWeapon;
        public System.Type previousState;

        private void Awake(){
            InitializeStateMachine();
        }
        
        // The InitializeStateMachine creates the dictionary that contains all the valid states that the behavior has
        protected abstract void InitializeStateMachine();
    }
}