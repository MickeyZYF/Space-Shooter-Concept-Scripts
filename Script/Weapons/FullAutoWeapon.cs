using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Weapons
{
    public class FullAutoWeapon : MZYF.Core.WeaponHandler
    {
        [Space]
        [SerializeField] protected float fireDelayTimer = 0f; // Time between weapon fire (For Automatic Weapons), essentially controls the fire-rate of the weapon
        [SerializeField] protected float fireDelay = 0f; // Used to clamp fireDelayTimer
        protected float FireDelayTimer { // The orthographic that the camera moves towards when the player ship is accelerating, decelerating, or boosting
            get { return fireDelayTimer; }
            set { fireDelayTimer = Mathf.Clamp(value, 0, fireDelay); }
        }

        [Space]
        [SerializeField] protected float weaponSpread = 0f;

        void Update(){
            FireDelayTimer -= Time.deltaTime;
        }

        public override void fire(){
            if (FireDelayTimer > 0){
                return;
            }

            if (altBarrel == null){
                launchProjectile(barrel);
            }
            else {
                alternatingFire();
            }
        }

        protected void alternatingFire(){
            if (linked){
                launchProjectile(barrel);
                launchProjectile(altBarrel);
                return;
            }

            if (alternate){
                launchProjectile(altBarrel);
                alternate = false;
            }
            else {
                launchProjectile(barrel);
                alternate = true;
            }
        }
        protected virtual void launchProjectile(GameObject barrel){
            Instantiate(projectile, barrel.transform.position, weaponSpreadCalculator(barrel.transform.rotation));
            FireDelayTimer = fireDelay;
        }

        protected Quaternion weaponSpreadCalculator(Quaternion barrelAngle){
            Vector3 barrelAngleVector = barrelAngle.eulerAngles;
            float angleRange = Random.Range(barrelAngleVector.z - weaponSpread, barrelAngleVector.z + weaponSpread);
            Quaternion weaponSpreadAngle = Quaternion.Euler (barrelAngleVector.x, barrelAngleVector.y, angleRange);
            
            return weaponSpreadAngle;
        }
    }
}