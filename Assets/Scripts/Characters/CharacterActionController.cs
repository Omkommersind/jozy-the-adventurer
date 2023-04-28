using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionController : MonoBehaviour
{
    public LayerMask mask;
    private bool _actionIntent = false;
    private BoxCollider2D _boxCollider2D;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _actionIntent = true;
        }
    }

    private void FixedUpdate()
    {
        if (_actionIntent)
        {
            _actionIntent = false;
            DoAct();
        }
    }

    void DoAct()
    {
        // First check if there is item under the player and act with it (take most of times)
        List<Collider2D> collisions = new List<Collider2D>();

        ContactFilter2D contactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = mask,
            useTriggers = true
        };

        int collisionsCount = _boxCollider2D.OverlapCollider(contactFilter, collisions);
        if (collisionsCount > 0)
        {
            foreach (Collider2D collision in collisions)
            {
                var item = collision.gameObject.GetComponent<InventoryItem>();
                item.PutIntoInventory();
            }
        } else
        {
            // Try use item from inventory / put it on ground
        }
    }
}
