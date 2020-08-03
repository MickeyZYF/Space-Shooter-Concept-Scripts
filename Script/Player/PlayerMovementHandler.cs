using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 movement;
        [SerializeField] protected PlayerShipMovement shipMovement;
        [SerializeField] protected PlayerButterflyMovement butterflyMovement;
        [SerializeField] protected PlayerResourceHandler resourceHandler; 
        [SerializeField] protected float maxVelocity = 0f; 
        private bool butterflyMode; 
        private bool butterflyDash; // Checks if the boost button is pressed in butterfly mode, used since FixedUpdate dosen't play well with GetButtonDown
        public static bool velocityChanging = false; // Checks if the ship is not a normal speed, used in cameraScroll
        public delegate void CameraZoom(string Speed);
        public static event CameraZoom Zoomed; 

        // public delegate bool ResourceConsume(float resourceCost);
        // public static event ResourceConsume ResourceConsumed; 
        public float boostCost;
        public float dashCost; 

        void Awake(){
            rb = this.GetComponent<Rigidbody2D>();
        }

        void Start(){
            boostCost = boostCost * Time.deltaTime; 
        }

        // GetButton Up and Down don't work properly in FixedUpdate, check for them in Update, have them set a bool that would then be checked in FixedUpdate
        void Update(){    
            if (butterflyMode){
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");   
            }
            TransformCheck();
            ButterflyDashCheck();
        }

        void FixedUpdate(){
            if (butterflyMode){
                butterflyMovement.ShipRotation();
                ButterflyMovement();
            }
            else {
                shipMovement.ShipRotation();
                ShipMovement();
            }
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }

        void ShipMovement(){
            if (Input.GetButton("Accelerate")){
                if (Input.GetButton("Boost") && resourceHandler.ParticleCheck(boostCost)){
                    shipMovement.Boost(rb);
                    resourceHandler.ParticleConsume(boostCost);
                    Zoomed.Invoke("Boost");
                }
                else {
                    shipMovement.Accelerate(rb);
                    Zoomed.Invoke("Accelerate");
                }
            }
            else if (Input.GetButton("Decelerate")){
                shipMovement.Decelerate(rb);
                Zoomed.Invoke("Decelerate");
            }
            else {
                shipMovement.Cruise(rb);
            }

            if (!InputCheck()){
                Zoomed.Invoke("Cruise");
            }
        }

        // Can't combine the two movement functions as boost has different behavior betweent modes
        // You can only boost while accelerating in ship mode, while you can dash whenever in butterfly mode
        void ButterflyMovement(){
            if (AxisCheck()){
                if (butterflyDash && resourceHandler.ParticleCheck(boostCost)){
                    butterflyMovement.Boost(rb, movement);
                    resourceHandler.ParticleConsume(dashCost);
                    butterflyDash = false;
                }

                if (Input.GetButton("Accelerate")){
                    butterflyMovement.Accelerate(rb, movement);
                    Zoomed.Invoke("Accelerate");
                }
                else if (Input.GetButton("Decelerate")){
                    butterflyMovement.Decelerate(rb, movement);
                    Zoomed.Invoke("Decelerate");
                }
                else {
                    butterflyMovement.Cruise(rb, movement);
                }
            }

            if (!AxisCheck() || !InputCheck()){
                Zoomed.Invoke("Cruise");
            }
        }

        void TransformCheck(){
            if (Input.GetButtonDown("Transform")){
                butterflyMode = !butterflyMode;
            }
        }

        void ButterflyDashCheck(){
            if (butterflyMode && AxisCheck()){
                if (Input.GetButtonDown("Boost")){
                    butterflyDash = true;
                }
            }
        }
        bool InputCheck(){
            if (!(Input.GetButton("Accelerate")) && !(Input.GetButton("Decelerate"))){
                return false;
            }
            else {
                return true; 
            }
        }

        bool AxisCheck(){
            if (movement == Vector2.zero){
                return false;
            }
            else {
                return true; 
            }
        }

        public void Recoil(float weaponRecoil){
            rb.AddForce(this.transform.right * weaponRecoil * -1f);
        }
    }
}