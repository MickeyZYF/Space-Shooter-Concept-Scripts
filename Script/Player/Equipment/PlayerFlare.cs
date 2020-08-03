using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public class PlayerFlare : MonoBehaviour
    {
        [SerializeField] protected float flare;
        [SerializeField] protected float flareRechargeRate;

        void Start(){
            flare = 100f; 
        }
        
        void Update(){
            if (flare == 100f){
                if (Input.GetButtonDown("Flare")){
                    Debug.Log("Flare!");
                    flare = 0f;
                }
            }
            else {
                flare += flareRechargeRate * Time.deltaTime;
                flare = Mathf.Clamp(flare, 0f, 100f);
            }
        }
    }
}