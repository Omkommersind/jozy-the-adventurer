using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseGroundCheck))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharecterDirectionController))]
public class CharacterMovement : MonoBehaviour
{
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


        if (_deltaX < 0 && _charecterDirectionController.FaceRight || 
            _deltaX > 0 && !_charecterDirectionController.FaceRight)
        {
            _charecterDirectionController.Flip();
        }
        _animator.SetFloat("speed", Mathf.Abs(_deltaX));
    }

    private void FixedUpdate()
    {
        var isGrounded = _grounded.IsGrounded;

        if (_deltaX != .0f)
        {
            _rb.velocity = new Vector2(_deltaX, _rb.velocity.y);
            _rb.gravityScale = _initGravityScale;
        } else if (isGrounded)
        {
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0;
        }

        if (_jumpIntent && isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


        _animator.SetFloat("verticalSpeed", _rb.velocity.y);
        _animator.SetBool("grounded", isGrounded);
        _jumpIntent = false;
        _deltaX = .0f;

        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, _deltaX);
    }
}
