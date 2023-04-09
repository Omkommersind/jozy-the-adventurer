using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseGroundCheck))]
public class CharacterMovement : MonoBehaviour
{
    public float Speed = 250.0f;
    public float jumpForce = 12.0f;

    private bool _jumpIntent = false;
    private float _deltaX = .0f;
    private BaseGroundCheck _grounded;

    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _grounded = _rb.GetComponent<BaseGroundCheck>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpIntent = true;
        }
        _deltaX += Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_deltaX != .0f)
        {
            Vector2 movement = new Vector2(_deltaX, _rb.velocity.y);
            _rb.velocity = movement;
        }

        if (_jumpIntent && _grounded.IsGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        _jumpIntent = false;
        _deltaX = .0f;
    }
}
