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
            _animator.SetFloat("speed", Mathf.Abs(_deltaX));

            if (_jumpIntent)
            {
                UpdateGravityScale(_initGravityScale);
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            } else
            {
                if (_deltaX != .0f)
                {
                    UpdateGravityScale(.0f);
                    _rb.velocity = new Vector2(_deltaX, _rb.velocity.y);
                } else
                {
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
                    UpdateGravityScale(.0f);
                }
            }
        }
        else
        {
            UpdateGravityScale(_initGravityScale);
        }

        _animator.SetFloat("verticalSpeed", _rb.velocity.y);
        _animator.SetBool("grounded", isGrounded);
        _jumpIntent = false;
        _deltaX = .0f;

        //Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, _deltaX);
    }

    void UpdateGravityScale(float value)
    {
        _rb.gravityScale = 1;
        // Todo
        //if (_rb.gravityScale != value)
        //{
        //    _rb.gravityScale = value;
        //}
    }
}
