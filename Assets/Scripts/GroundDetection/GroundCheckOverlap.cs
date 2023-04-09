using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckOverlap : BaseGroundCheck
{
    public float CheckHeight = 0.1f;
    public LayerMask mask;

    [SerializeField]
    private bool _isGrounded = false;
    private Vector3 _max;
    private Vector3 _min;
    private Vector2 _corner1;
    private Vector2 _corner2;
    private BoxCollider2D _boxCollider2D;

    public override bool IsGrounded { get => _isGrounded; }

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        _max = _boxCollider2D.bounds.max;
        _min = _boxCollider2D.bounds.min;
        var edgeRadius = _boxCollider2D.edgeRadius;

        _corner1 = new Vector2(_min.x - edgeRadius, _min.y - edgeRadius);
        _corner2 = new Vector2(_max.x + edgeRadius, _min.y + edgeRadius + CheckHeight);
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
        Gizmos.color = new Color(0f, 1f, 0f, 1f);
        Gizmos.DrawWireCube(new Vector2((_corner1.x + _corner2.x) / 2, _corner1.y), 
            new Vector2(_corner2.x - _corner1.x, CheckHeight));
    }
}
