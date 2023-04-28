using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterActionController : MonoBehaviour
{
    public LayerMask mask;

    private Inventory _inventory;
    private bool _actionIntent = false;
    private BoxCollider2D _boxCollider2D;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _inventory = GetComponent<Inventory>();
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
            var used = _inventory.UseCurrentItem(new Vector2(_boxCollider2D.bounds.center.x, 
                _boxCollider2D.bounds.min.y - _boxCollider2D.edgeRadius));
        }
    }
}
