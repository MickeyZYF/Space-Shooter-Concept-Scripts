using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MZYF.AI
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<System.Type, State> availableStates; // Used to keep a "dictionary" of all the available/valid states for the State Machine
        public State currentState { get; private set; }

        // Update calls the tick function of the current state
        // tick returns null if it wants to stay as the current stay, returns the type of the next state if the current state wishes to switch
        private void Update(){
            if (currentState == null){
                currentState = availableStates.Values.First();
            }

            var nextState = currentState?.Tick();

            if (nextState != null && 
                    nextState != currentState?.GetType()){
                SwitchState(nextState);
            }
        }

        public void SetStates(Dictionary<System.Type, State> states){
            availableStates = states;
        }
        
        private void SwitchState(System.Type state){
            currentState = availableStates[state];
        }
    }
}