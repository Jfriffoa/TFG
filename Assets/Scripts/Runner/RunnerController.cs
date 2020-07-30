using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Runner {
    public class RunnerController : MonoBehaviour {
        [Header("Movevement Parameters")]
        public float jumpForce = 1f;
        public float speed = 5f;

        [Header("Damage and Recovery")]
        public float recoilTime = 3f;
        float _originalSpeed;
        float _hittedTime;
        bool _isRecovering = false;

        [Header("Grounded Parameters")]
        public Transform groundCheck;
        public float groundCheckDistance = 0.1f;
        bool _isGrounded;

        // Player Parameters
        Rigidbody2D _rb;
        Animator _animator;
        Vector2 _initialPos;


        void Start() {
            _originalSpeed = speed;
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _initialPos = transform.position;
        }

        void Update() {
            // Check if Grounded
            CheckGround();

            // Apply a recoil each time we hit an obstacle
            if (_isRecovering) {
                var t = (Time.time - _hittedTime) / recoilTime;
                speed = Mathf.Lerp(0, _originalSpeed, t);
                if (t >= 1)
                    _isRecovering = false;

                _animator.SetFloat("RelativeSpeed", speed / _originalSpeed);
            }
        }

        void FixedUpdate() {
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
        }

        public void Jump() {
            if (_isGrounded) {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Debug.Log("Jumping", gameObject);
            }
        }

        public void ResetPos() {
            transform.position = _initialPos;
        }

        void CheckGround() {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance);
            _isGrounded = hit.collider != null;
            _animator.SetBool("Grounded", _isGrounded);
        }

        internal void TakeDamage() {
            if (_isRecovering)
                return;

            _animator.SetTrigger("Take Damage");
            _isRecovering = true;
            _hittedTime = Time.time;
            speed = 0f;
        }
    }
}