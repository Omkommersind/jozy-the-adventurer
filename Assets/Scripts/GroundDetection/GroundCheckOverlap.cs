using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GroundCheckOverlap : BaseGroundCheck
{
    public LayerMask mask;
    public float PaddingX = 0.1f;

    [SerializeField]
    private bool _isGrounded = false;
    private Vector3 _max;
    private Vector3 _min;
    private Vector2 _corner1;
    private Vector2 _corner2;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rb;

    public override bool IsGrounded { get => _isGrounded; }

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        Gizmos.color = new Color(0f, 1f, 0f, 1f);
    }

    void Update()
    {
        if (Mathf.Abs(_rb.velocity.y) > 0.05)
        {
            _isGrounded = false;
            return;
        }

        _max = _boxCollider2D.bounds.max;
        _min = _boxCollider2D.bounds.min;
        var edgeRadius = _boxCollider2D.edgeRadius;

        _corner1 = new Vector2(_min.x - edgeRadius + PaddingX, _min.y - edgeRadius);
        _corner2 = new Vector2(_max.x + edgeRadius - PaddingX, _min.y + edgeRadius + CheckHeight);
        Collider2D hit = Physics2D.OverlapArea(_corner1, _corner2, mask.value);

        _isGrounded = false;
        if (hit != null)
        {
            _isGrounded = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider2D == null) return;
        Gizmos.DrawWireCube(new Vector2((_corner1.x + _corner2.x) / 2, _corner1.y), 
            new Vector2(_corner2.x - _corner1.x, CheckHeight));
    }
}
