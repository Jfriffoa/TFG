using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Runner {
    public class RunnerController : MonoBehaviour {
        [Header("Movevement Parameters")]
        public float jumpForce = 1f;
        public float speed = 5f;

        [Header("Grounded Parameters")]
        public Transform groundCheck;
        public float groundCheckDistance = 0.1f;

        Rigidbody2D _rb;
        Vector2 _initialPos;

        void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _initialPos = transform.position;
        }

        void FixedUpdate() {
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
        }

        public void Jump() {
            if (IsGrounded()) {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Debug.Log("Jumping", gameObject);
            }
        }

        public void ResetPos() {
            transform.position = _initialPos;
        }

        bool IsGrounded() {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance);
            return hit.collider != null;
        }
    }
}