using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundCheckRaycast : BaseGroundCheck
{
    public bool ShowGroundCollisionGizmo = false;
    public LayerMask mask;

    private BoxCollider2D _boxCollider = null;
    private float _edgeRadius;
    private Vector2 direction = Vector2.down;
    private List<Vector2> _raysList = new List<Vector2>();
    [SerializeField]
    private bool _isGrounded = false;

    public override bool IsGrounded { get => _isGrounded; }

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _edgeRadius = _boxCollider.edgeRadius;
    }

    private void Update()
    {
        _raysList = new List<Vector2>();
        // Left side ray
        _raysList.Add(new Vector3(_boxCollider.bounds.min.x - _edgeRadius, _boxCollider.bounds.min.y, transform.position.z));

        // Center left ray
        _raysList.Add(new Vector3(_boxCollider.bounds.center.x - _edgeRadius, _boxCollider.bounds.min.y, transform.position.z));

        // Center ray
        _raysList.Add(new Vector3(_boxCollider.bounds.center.x, _boxCollider.bounds.min.y, transform.position.z));

        // Center right ray
        _raysList.Add(new Vector3(_boxCollider.bounds.center.x + _edgeRadius, _boxCollider.bounds.min.y, transform.position.z));

        // Right side ray
        _raysList.Add(new Vector3(_boxCollider.bounds.max.x + _edgeRadius, _boxCollider.bounds.min.y, transform.position.z));
    }

    private void FixedUpdate()
    {
        _isGrounded = false;
        foreach (Vector2 position in _raysList)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction, CheckHeight + _edgeRadius, mask);
            if (hit.collider != null)
            {
                _isGrounded = true;
                break;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (ShowGroundCollisionGizmo)
        {
            Gizmos.color = Color.green;
            foreach (var pos in _raysList)
            {
                Gizmos.DrawLine(pos, new Vector2(pos.x, pos.y - CheckHeight - _edgeRadius));
            }
        }
    }
}