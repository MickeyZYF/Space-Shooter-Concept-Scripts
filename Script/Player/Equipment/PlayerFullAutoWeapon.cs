using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player.Weapons
{
    public class PlayerFullAutoWeapon : MZYF.Weapons.FullAutoWeapon
    {
        [SerializeField] protected float weaponRecoil = 0f; 
        
        [Space]
        [SerializeField] protected PlayerMovementHandler playerMovement; 
        [SerializeField] protected PlayerResourceHandler resourceHandler;
        
        [Space]
        [SerializeField] protected bool physical = false; // If not physical, costs particles, if physical uses ammunition
        [SerializeField] protected float particleCost = 0f;
        [SerializeField] protected float maxAmmunition = 0f;
        [SerializeField] protected float ammunition;
        
        void Start(){
            ammunition = maxAmmunition;
        }
        
        public override void fire(){
            if (!resourceCheck()){
                return;
            }
            base.fire();
        }

        bool resourceCheck(){
            if (physical){
                if (ammunition > 0f){
                    return true;
                }
            }
            if (!physical){
                if (resourceHandler.ParticleCheck(particleCost)){
                    return true;
                }
            }
            return false;
        }

        void resourceConsume(){
            if (physical){
                ammunition--;
            }
            else {
                resourceHandler.ParticleConsume(particleCost);
            }
        }

        protected override void launchProjectile(GameObject barrel){
            base.launchProjectile(barrel);
            resourceConsume();
            playerMovement.Recoil(weaponRecoil);
        }
    }
}