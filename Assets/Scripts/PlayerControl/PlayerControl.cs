using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb = default;
        [SerializeField] private float controlSpeed = 15f;
        [SerializeField] private float rushForce = 50f;
        [SerializeField] private float jumpForce = 15f;

        private Vector3 _moveDir;
        private bool _canJump = true;
        
        private void Update()
        {
            HandleControl();
            // HandleRush();
            // HandleJump();
        }

        private void HandleControl()
        {
            _moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
                Input.GetAxisRaw("Vertical")) * controlSpeed;
            rb.velocity = _moveDir;
        }

        private void HandleRush()
        {
            if (!Input.GetMouseButtonDown(0)) { return; }
            
            rb.AddForce(_moveDir.normalized * rushForce, ForceMode.Impulse);
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