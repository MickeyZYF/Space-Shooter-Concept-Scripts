using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Core
{
    public class IFFDevice : MonoBehaviour
    {
        //public string faction = ""; // Can be "Player" "Ally", "Enemy", or "Environment"
        public enum Faction {
            Player,
            Ally,
            Enemy,
            Environment
        }
        public Faction faction;

        public enum Identification {
            SmallShip,
            LargeShip,
            Turret,
            Missile,
            Projectile,
            Hazard
        }
        public Identification identification; // Can be "Large Ship", "Small Ship", "Turret", "Missile", "Projectile", or "Hazard"

        public Identification[] validTargets;

        // returns true if the two objects are friendly/neutral, false if they are enemies
        // With the current way we set up layers, the colliders of enemies or allies can't actually hit itself, so we might not need the first if-condition
        public bool friendOrFoe(GameObject objectA, GameObject objectB){
            Faction factionA = objectA.GetComponent<IFFDevice>().faction;
            Faction factionB = objectB.GetComponent<IFFDevice>().faction;

            if (factionA == factionB){
                return true;
            }

            if ((factionA == Faction.Ally && factionB == Faction.Player) || 
                    (factionA == Faction.Player && factionB == Faction.Ally)){
                return true;
            } 

            if (factionA == Faction.Environment || factionB == Faction.Environment){
                return true;
            }

            return false; 
        }

        public bool checkIdentity(GameObject target, Identification identity){
            Identification targetIdentity = target.GetComponent<IFFDevice>().identification;
            if (targetIdentity == identity){
                return true;
            }
            else {
                return false;
            }
        }
    }
}