using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BaseGroundCheck))]
public class CharacterActionController : MonoBehaviour
{
    public LayerMask itemsMask;
    public LayerMask interactiveObjectsMask;
    public LayerMask interactiveObjectsWithItemMask;

    private Inventory _inventory;
    private bool _actionIntent = false;
    private BoxCollider2D _boxCollider2D;
    private BaseGroundCheck _grounded;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _inventory = GetComponent<Inventory>();
        _grounded = GetComponent<BaseGroundCheck>();
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
        if (_actionIntent && _grounded.IsGrounded)
        {
            _actionIntent = false;
            DoAct();
        }
    }

    void DoAct()
    {
        // First check if there is object that can be used without item
        var interactiveObjCollisions = GetCollisionsByMask(interactiveObjectsMask);
        if (interactiveObjCollisions.Count > 0)
        {
            foreach (Collider2D collision in interactiveObjCollisions)
            {
                IActionReceiver interactiveObject = interactiveObjCollisions[0].gameObject.GetComponent<IActionReceiver>();
                interactiveObject.Interact();
            }
            return;
        }

        // Second check if there is item under the player and act with it
        var itemsCollisions = GetCollisionsByMask(itemsMask);
        if (itemsCollisions.Count > 0)
        {
            foreach (Collider2D collision in itemsCollisions)
            {
                var item = collision.gameObject.GetComponent<InventoryItem>();
                item.PutIntoInventory();
            }
            return;
        }

        // Try use item from inventory / put it on ground
        var interactiveObjectsCollisions = GetCollisionsByMask(interactiveObjectsWithItemMask);
        var pos = new Vector2(_boxCollider2D.bounds.center.x,
            _boxCollider2D.bounds.min.y - _boxCollider2D.edgeRadius);
        var used = _inventory.UseCurrentItem(pos, interactiveObjectsCollisions);
    }

    private List<Collider2D> GetCollisionsByMask(LayerMask mask)
    {
        List<Collider2D> collisions = new List<Collider2D>();

        ContactFilter2D contactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = mask,
            useTriggers = true
        };

        int collisionsCount = _boxCollider2D.OverlapCollider(contactFilter, collisions);
        return collisions;
    }
}
