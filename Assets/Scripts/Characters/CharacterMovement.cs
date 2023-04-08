using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 250.0f;
    public float jumpForce = 12.0f;

    private bool _jumpIntent = false;
    private float _deltaX = .0f;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Vector2 movement = new Vector2(_deltaX, rb.velocity.y);
            rb.velocity = movement;
        }

        if (_jumpIntent)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        _jumpIntent = false;
        _deltaX = .0f;
    }
}
