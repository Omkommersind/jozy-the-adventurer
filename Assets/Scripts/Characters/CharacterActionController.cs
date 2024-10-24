using System.Collections.Generic;
using Assets.Scripts.Inventory;
using Assets.Scripts.Inventory.ActionRecievers;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(global::Inventory))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(BaseGroundCheck))]
    public class CharacterActionController : MonoBehaviour
    {
        public LayerMask ItemsMask;
        public LayerMask InteractiveObjectsMask;
        public LayerMask InteractiveObjectsWithItemMask;

        [SerializeField]
        private global::Inventory _inventory;
        private bool _actionIntent = false;
        private BoxCollider2D _boxCollider2D;
        private BaseGroundCheck _grounded;

        void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _inventory = GetComponent<global::Inventory>();
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
                Act();
            }
        }

        void Act()
        {
            // First check if there is object that can be used without itemView
            var interactiveObjCollisions = GetCollisionsByMask(InteractiveObjectsMask);
            if (interactiveObjCollisions.Count > 0)
            {
                foreach (Collider2D collision in interactiveObjCollisions)
                {
                    IActionReceiver interactiveObject = interactiveObjCollisions[0].gameObject.GetComponent<IActionReceiver>();
                    interactiveObject.Interact();
                }
                return;
            }

            // Second check if there is itemView under the player and act with it
            var itemsCollisions = GetCollisionsByMask(ItemsMask);
            if (itemsCollisions.Count > 0)
            {
                foreach (Collider2D collision in itemsCollisions)
                {
                    var item = collision.gameObject.GetComponent<ItemView>();
                    item.Data.PutIntoInventory();
                }
                return;
            }

            // Try use itemView from inventory / put it on ground
            var interactiveObjectsCollisions = GetCollisionsByMask(InteractiveObjectsWithItemMask);
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
}
