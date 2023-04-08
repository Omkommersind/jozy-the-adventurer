using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public float CheckHeight = 0.1f;
    [SerializeField]
    private bool _isGrounded = false;
    private Vector3 _max;
    private Vector3 _min;
    private Vector2 _corner1;
    private Vector2 _corner2;
    private BoxCollider2D _boxCollider2D;

    public bool IsGrounded { get => _isGrounded; }

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _max = _boxCollider2D.bounds.max;
        _min = _boxCollider2D.bounds.min;
        _corner1 = new Vector2(_max.x, _min.y);
        _corner2 = new Vector2(_min.x, _min.y - CheckHeight);
    }

    void Update()
    {
        Collider2D hit = Physics2D.OverlapArea(_corner1, _corner2);

        _isGrounded = false;
        if (hit != null)
        {
            _isGrounded = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 1f);
        Gizmos.DrawWireCube(new Vector3(_max.x / 2, _max.y / 2, 0.01f), new Vector3(_max.x / 2, _max.y / 2, 0.01f));
    }
}
