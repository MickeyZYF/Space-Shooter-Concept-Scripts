using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public class PlayerEquipmentHandler : MonoBehaviour
    {
        [SerializeField] protected MZYF.Core.WeaponHandler primaryWeapon;
        [SerializeField] protected MZYF.Core.WeaponHandler secondaryWeapon;
        [SerializeField] protected MZYF.Core.WeaponHandler flare;
        [SerializeField] protected MZYF.Core.WeaponHandler special;
        [SerializeField] protected MZYF.Core.WeaponHandler utility;
        public bool linked = false; 


        void Update(){
            if (Input.GetButton("Primary Weapon")){
                primaryWeapon?.fire(); 
            }

            if (Input.GetButton("Secondary Weapon")){
                secondaryWeapon?.fire();
            }

            if (Input.GetButton("Flare")){
                flare?.fire();
            }

            if (Input.GetButton("Special")){
                special?.fire();
            }

            if (Input.GetButton("Utility")){
                utility?.fire();
            }

            if (Input.GetButton("Link Weapons")){
                linked = !linked; 
            }
        }
    }
}