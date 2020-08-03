using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public abstract class PlayerWeaponHandler : MZYF.Core.WeaponHandler
    {
        [Space]
        public PlayerResourceHandler resourceHandlerScript;
        public PlayerMovementHandler playerMovementScript; 
        
        [Space]
        public bool physical = false; // If not physical, costs particles, if physical uses ammunition
        public float particleCost = 0f;
        public float maxAmmunition = 0f;
        public float ammunition;
    }
}