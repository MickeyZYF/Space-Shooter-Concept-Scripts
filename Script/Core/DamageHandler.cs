using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Core {
    public class DamageHandler : MonoBehaviour
        {
            // This script handles the health/damage of gameobjects that are not the player, so Ally, Enemy, Environment 
            [SerializeField] protected float damage = 0f; // This is the amount of damage the gameobject inflicts on collision against other gameobjects
            
            [Space] 
            [SerializeField] protected float health; // The current health of the gameobject
            [SerializeField] protected float maxHealth = 1f; 
            protected float Health { // The orthographic that the camera moves towards when the player ship is accelerating, decelerating, or boosting
                get { return health; }
                set { health = Mathf.Clamp(value, 0, maxHealth); }
            }
            
            protected virtual void Start(){
                health = maxHealth;
            }

            // For damage handler, we have the the script affect it's own health variable
            // We retrieve the damage value from the other DamageHandler script and deduct it from the above health variable 
            // Projectiles fired by the player have the "Ally Tag" 
            protected void OnCollisionEnter2D(Collision2D collidedObj){
                if (collidedObj.gameObject != null){
                    DamageCalculator(collidedObj, false);
                }
            }

            protected void OnCollisionStay2D(Collision2D collidedObj){
                if (collidedObj.gameObject != null){
                    DamageCalculator(collidedObj, true);
                }
            }

            protected virtual void TakeDamage(bool friendlyFire, float damage){
                if (friendlyFire){ 
                    // Friendly fire does only 10 percent damage
                    Health -= (damage * 0.1f);
                }
                else {
                    Health -= damage;
                }
                DeathCheck();
            }

            protected void DeathCheck(){
                if (Health <= 0){
                    Destroy(this.gameObject);
                }
            }

            protected void DamageCalculator(Collision2D damageSource, bool continuous){
                float damageTaken;
                damageTaken = damageSource.gameObject.GetComponent<DamageHandler>().damage;

                bool friendlyFire;

                // This basically ensures that projectiles and missles still get destroyed on impact with friendlies and dosen't just bounce off
                MZYF.Core.IFFDevice IFF = this.GetComponent<MZYF.Core.IFFDevice>();
                if (IFF.checkIdentity(this.gameObject, MZYF.Core.IFFDevice.Identification.Missile) || 
                        IFF.checkIdentity(this.gameObject, MZYF.Core.IFFDevice.Identification.Projectile)){
                    friendlyFire = false;
                }
                else {
                    friendlyFire = IFF.friendOrFoe(this.gameObject, damageSource.gameObject);
                }

                if (continuous){
                    TakeDamage(friendlyFire, damageTaken * Time.deltaTime);
                }
                else {
                    TakeDamage(friendlyFire, damageTaken);  
                }
            }
    }
}