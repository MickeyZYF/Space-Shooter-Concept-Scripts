using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MZYF.Camera
{
    public class CameraScroll : MonoBehaviour
    {
        [SerializeField] protected UnityEngine.Camera mainCamera; 

        [Space]
        [SerializeField] private float targetOrtho;        
        private float TargetOrtho { // The orthographic that the camera defaults to. Only changed by scrolling (ScrollZoom)
            get { return targetOrtho; }
            set { targetOrtho = Mathf.Clamp(value, minOrtho, maxOrtho); }
        }
        [SerializeField] private float temporaryOrtho;
        private float TemporaryOrtho { // The orthographic that the camera moves towards when the player ship is accelerating, decelerating, or boosting
            get { return temporaryOrtho; }
            set { temporaryOrtho = Mathf.Clamp(value, minOrtho - tempZoomDelta, maxOrtho + tempZoomDelta); }
        }
        [SerializeField] private float minOrtho = 2.5f;
        [SerializeField] private float maxOrtho = 7.5f;

        [Space]
        [SerializeField] private float scrollZoomSpeed = 1f;
        [SerializeField] private float tempZoomSpeed = 0.25f;
        [SerializeField] private float tempZoomDelta = 0.25f; // How much the camera zooms in and out when the player ship is accelerating, decelerating, or boosting
        [SerializeField] private float tempZoomModifier = 2f; // Used to modifiy speedChangeZoomDelta when the player ship is boosting

        private bool speedChange; 
        
        void OnEnable(){
            MZYF.Player.PlayerMovementHandler.Zoomed += speedChangeZoom;
        }

        void OnDisable(){
            MZYF.Player.PlayerMovementHandler.Zoomed -= speedChangeZoom;
        }

        void Start(){
            mainCamera = this.GetComponent<UnityEngine.Camera>();
            TargetOrtho = mainCamera.orthographicSize;
            TemporaryOrtho = TargetOrtho; 
        }

        void Update(){ 
            ScrollZoom();
            //MZYF.Player.PlayerMovementHandler.velocityChanging
            if (!(speedChange)){
                mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, TargetOrtho, Time.deltaTime);  
                TemporaryOrtho = TargetOrtho;
            }
        }

        public void ScrollZoom(){
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f) {
                TargetOrtho -= scroll * scrollZoomSpeed;
            }
        }

        public void speedChangeZoom(string velocityChange){
            switch (velocityChange){
                case "Cruise":
                speedChange = false;
                break;
                case "Accelerate":
                TemporaryOrtho = TargetOrtho + tempZoomDelta;
                speedChange = true;
                break;
                case "Decelerate":
                TemporaryOrtho = TargetOrtho - tempZoomDelta;
                speedChange = true;
                break;
                case "Boost":
                TemporaryOrtho = TargetOrtho + (tempZoomDelta * tempZoomModifier);
                speedChange = true;
                break;
            }
            mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, TemporaryOrtho, tempZoomSpeed * Time.deltaTime);  
        }
    }
}