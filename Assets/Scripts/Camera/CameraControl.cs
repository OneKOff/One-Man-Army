using UnityEngine;

namespace MyCamera
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget = default;
        [SerializeField] private float rotationSpeed = 180f;
        [SerializeField] private float sensitivity = 1f;

        private Vector3 _relativePos;
        private Vector3 _prevMousePos;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            _relativePos = transform.position - Camera.main.transform.position;
            _prevMousePos = Input.mousePosition;
        }

        private void Update()
        {
            transform.position = cameraTarget.position;
            
            var deltaMousePos = Input.mousePosition - _prevMousePos;
            _prevMousePos = Input.mousePosition;

            // transform.Rotate(-deltaMousePos.y / Camera.main.rect.width * 360f * sensitivity * Time.deltaTime, 
            //                      -deltaMousePos.x / Camera.main.rect.height * 360f * sensitivity * Time.deltaTime, 
            //                      0, 
            //                      Space.Self);
            
            // transform.Rotate(0,  
            //     -deltaMousePos.x / Camera.main.rect.height * 360f * sensitivity * Time.deltaTime, 
            //     0, 
            //     Space.World);
        }
    }
}