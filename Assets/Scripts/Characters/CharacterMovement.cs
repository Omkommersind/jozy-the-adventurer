using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseGroundCheck))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharecterDirectionController))]
public class CharacterMovement : MonoBehaviour
{
    // Todo: max horizontal velocity
    public float Speed = 250.0f;
    public float SpeedInAirModifier = 0.1f;
    public float jumpForce = 12.0f;

    private bool _jumpIntent = false;
    private float _deltaX = .0f;
    private BaseGroundCheck _grounded;
    private Animator _animator;
    private CharecterDirectionController _charecterDirectionController;
    private float _initGravityScale;

    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _grounded = GetComponent<BaseGroundCheck>();
        _animator = GetComponent<Animator>();
        _charecterDirectionController = GetComponent<CharecterDirectionController>();
        _initGravityScale = _rb.gravityScale;
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
        var isGrounded = _grounded.IsGrounded;

        if (isGrounded)
        {
            if (_deltaX < 0 && _charecterDirectionController.FaceRight ||
                _deltaX > 0 && !_charecterDirectionController.FaceRight)
            {
                _charecterDirectionController.Flip();
            }

            if (_jumpIntent)
            {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                if (_deltaX != .0f)
                {
                    SetHorizontalVelocity(_deltaX);
                }
                else
                {
                    SetHorizontalVelocity(0);
                }
            }
        } else
        {
            if ((_rb.velocity.x > .0f && _deltaX < .0f) ||
                (_rb.velocity.x < .0f && _deltaX > .0f))
            {
                _rb.AddForce(new Vector2(_deltaX * SpeedInAirModifier, 0), ForceMode2D.Impulse);
            }
        }

        _animator.SetFloat("speed", Mathf.Abs(_deltaX));
        _animator.SetFloat("verticalSpeed", _rb.velocity.y);
        _animator.SetBool("grounded", isGrounded);
        _jumpIntent = false;
        _deltaX = .0f;

        //Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, _deltaX);
    }

    void SetHorizontalVelocity(float value)
    {
        _rb.velocity = new Vector2(value, _rb.velocity.y);
    }
}
