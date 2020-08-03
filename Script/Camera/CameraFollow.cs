using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MZYF.Camera 
{
    public class CameraFollow : MonoBehaviour
    {   
        [SerializeField] protected Transform target; // Target the camera is following
        
        [SerializeField] private float CameraZ = -10f;

        void Start(){
            target = GameObject.FindWithTag("Player").transform;
        }

        void Update(){
            if (target == null){
                return;
            }

            this.transform.position = new Vector3(target.position.x, target.position.y, CameraZ);
        }
    }
}