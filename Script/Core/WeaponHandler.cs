using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Core {
    public abstract class WeaponHandler : MonoBehaviour
    {        
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected GameObject barrel;
        [SerializeField] protected GameObject altBarrel; 
        [SerializeField] protected bool linked = false; 
        protected bool alternate = false; 

        public abstract void fire();

    }
}