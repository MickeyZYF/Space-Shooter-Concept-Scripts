using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public class PlayerResourceHandler : MonoBehaviour
    {
        [SerializeField] protected float particles;
        [SerializeField] protected float maxParticles = 1f;
        protected float Particles { // The orthographic that the camera moves towards when the player ship is accelerating, decelerating, or boosting
            get { return particles; }
            set { particles = Mathf.Clamp(value, 0, maxParticles); }
        }
        public float particleRechargeRate = 0f; 
        
        void Start(){       
            Particles = maxParticles;
        }

        void Update(){
            Particles += particleRechargeRate * Time.deltaTime;      
        }

        // Checks if there is enough particles to be used
        public bool ParticleCheck(float particleCost){
            if (Particles < particleCost){
                Debug.Log("Not Enough Particles!");
                return false;
            }
            else {
                return true;
            }
        }

        public void ParticleConsume(float particleCost){
            Particles -= particleCost;
        }
    }
}