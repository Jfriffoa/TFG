using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    [Header("Movevement Parameters")]
    public float jumpForce = 1f;
    public float speed = 5f;

    [Header("Grounded Parameters")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.1f;

    Rigidbody2D _rb;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
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

    bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance);
        return hit.collider != null;
    }
}
