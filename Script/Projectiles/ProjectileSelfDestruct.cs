using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Projectile
{
    public class ProjectileSelfDestruct : MonoBehaviour
    {
        [SerializeField] private float timer = 1f; // Would effectively be the max. range of the projectile

        void Update(){
            timer -= Time.deltaTime;
            if (timer <= 0f){
                Destroy(this.gameObject);
            }
        }
    }
}