using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxItems = 3;
    private readonly List<ItemView> _items = new List<ItemView>();

    private void Awake()
    {
        Messenger<ItemView>.AddListener(GameEvent.ITEM_PUT_TO_INVENTORY, AddItem);  // Todo: simple method
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RollInventoryRight();
        }
    }

    public List<ItemView> GetItems()
    {
        return _items;
    }

    public void AddItem(ItemView itemView)
    {
        if (_items.Count >= MaxItems) return; // If max items - put last itemView from inventory on ground
        if (_items.Contains(itemView)) return;

        _items.Add(itemView);
        itemView.gameObject.SetActive(false);
        MessageInventoryChanged();
    }

    public bool RemoveItem(ItemView itemView)
    {
        int deleted = _items.RemoveAll(r => r.name == itemView.name);
        if (deleted > 0)
        {
            MessageInventoryChanged();
            return true;
        }
        return false;
    }

    public bool UseCurrentItem(Vector2 position, List<Collider2D> interactiveObjectsCollisions)
    {
        if (_items.Count < 1) return false;

        ItemView itemView = GetCurrentItem();
        if (itemView == null) return false;

        if (interactiveObjectsCollisions != null && interactiveObjectsCollisions.Count > 0)
        {
            IActionReceiver interactiveObject = interactiveObjectsCollisions[0].gameObject.GetComponent<IActionReceiver>();
            bool success = interactiveObject.Interact(itemView);
            if (success)
            {
                RemoveItem(itemView);
                itemView.Use();
                MessageInventoryChanged();
            }
            return success;
        }

        // Deploy
        itemView.PutInWorld(position);
        _items.RemoveAt(0);
        MessageInventoryChanged();
        return true;
    }

    private void RollInventoryRight()
    {
        if (_items.Count > 1)
        {
            var item = GetCurrentItem();
            _items.RemoveAt(0);
            _items.Add(item);
            MessageInventoryChanged();
        }
    }

    private void MessageInventoryChanged()
    {
        Messenger<List<ItemView>>.Broadcast(GameEvent.INVENTORY_CHANGED, _items);
    }

    private ItemView GetCurrentItem()
    {
        if (_items.Count == 0) return null;
        return _items[0];
    }
}
