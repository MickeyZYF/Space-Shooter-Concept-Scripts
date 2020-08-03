using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public class PlayerDamageHandler : MZYF.Core.DamageHandler
    {
        [SerializeField] protected float shields;
        [SerializeField] protected float maxShields = 1f; // Used to clamp shields variable
        protected float Shields { // The orthographic that the camera moves towards when the player ship is accelerating, decelerating, or boosting
            get { return shields; }
            set { shields = Mathf.Clamp(value, 0, maxShields); }
        }

        [Space]
        [SerializeField] protected float shieldRechargeDelayTimer; // How long before shields starts recharging, resets to shieldRechargeDelay whenever damage is taken
        [SerializeField] protected float shieldRechargeDelay = 5f; // Used to clamp shieldRechargeTimer variable 
        protected float ShieldRechargeDelayTimer {
            get { return shieldRechargeDelayTimer; }
            set { shieldRechargeDelayTimer = Mathf.Clamp(value, 0, shieldRechargeDelay); }
        }
        [SerializeField] protected float shieldRechargeRate = 0f;

        // Start is called before the first frame update
        protected override void Start(){
            base.Start();
            Shields = maxShields;
        }

        // Update is called once per frame
        void Update(){ 
            ShieldRecharge();
        }

        void ShieldRecharge(){
            if (ShieldRechargeDelayTimer > 0){
                ShieldRechargeDelayTimer -= Time.deltaTime;
            }
            else {
                Shields += shieldRechargeRate * Time.deltaTime;
            }
        }

        void ShieldRechargeTimerStart(){
            ShieldRechargeDelayTimer = shieldRechargeDelay;
        }

        // The below implementation of TakeDamage allows the shield to completely absorbs the last tick of damage, with no overflow to health
        protected override void TakeDamage(bool friendlyFire, float damage){
            if (Shields > 0f){
                Shields -= damage;
                ShieldRechargeTimerStart();
            }
            else {
                health -= damage;
                ShieldRechargeTimerStart();
                DeathCheck();
            }      
        }
    }
}