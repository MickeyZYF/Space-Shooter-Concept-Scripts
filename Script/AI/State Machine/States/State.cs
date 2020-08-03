using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.AI
{
    public abstract class State
    {
        protected GameObject gameObject;
        protected Transform transform;
        protected Behavior behavior; 

        public State(GameObject gameObject){
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
            this.behavior = gameObject.GetComponent<Behavior>();
        }

        // Tick returns null if the current state dose not wish to be changed, and returns the type of the next state if it does
        // Tick also handles what the state does
        public abstract System.Type Tick();
    }
}