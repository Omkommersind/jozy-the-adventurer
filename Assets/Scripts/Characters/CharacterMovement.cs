using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 250.0f;
    public float jumpForce = 12.0f;

    private bool _jumpIntent = false;

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
    }

    private void FixedUpdate()
    {
        float deltaX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, rb.velocity.y);
        rb.velocity = movement;

        if (_jumpIntent)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        _jumpIntent = false;
    }
}
