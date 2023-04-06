using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float maxSpeed = 15f;
        [SerializeField] private float acceleration = 25f;
        [SerializeField] private float rushForce = 50f;
        [SerializeField] private float jumpForce = 15f;

        private Vector3 _movement, _targetMovement;
        private bool _canJump = true;
        
        private void Update()
        {
            HandleControl();
            // HandleRush();
            // HandleJump();
        }

        private void HandleControl()
        {
            _targetMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 
                Input.GetAxisRaw("Vertical")) * maxSpeed;
            _movement = Vector3.Lerp(_movement, _targetMovement, Time.deltaTime * acceleration);

            _movement = ClampVector(_movement, maxSpeed);
            rb.velocity = _movement;
        }

        private Vector3 ClampVector(Vector3 vector, float maxMagnitude)
        {
            float innateMagnitude = vector.magnitude;

            if (innateMagnitude > maxMagnitude)
            {
                vector = vector.normalized * maxMagnitude;
            }

            return vector;
        }

        private void HandleRush()
        {
            if (!Input.GetMouseButtonDown(0)) { return; }
            
            rb.AddForce(_movement.normalized * rushForce, ForceMode.Impulse);
        }

        private void HandleJump()
        {
            if (!Input.GetKeyDown(KeyCode.Space) || !_canJump) { return; }
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _canJump = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            _canJump = true;
        }
    }
}