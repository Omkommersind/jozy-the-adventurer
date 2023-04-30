using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxItems = 3;
    private readonly List<InventoryItem> _items = new List<InventoryItem>();

    private void Awake()
    {
        Messenger<InventoryItem>.AddListener(GameEvent.ITEM_PUT_TO_INVENTORY, AddItem);  // Todo: simple method
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RollInventoryRight();
        }
    }

    public List<InventoryItem> GetItems()
    {
        return _items;
    }

    public void AddItem(InventoryItem item)
    {
        if (_items.Count >= MaxItems) return; // If max items - put last item from inventory on ground
        if (_items.Contains(item)) return;

        _items.Add(item);
        item.gameObject.SetActive(false);
        MessageInventoryChanged();
    }

    public bool RemoveItem(InventoryItem item)
    {
        int deleted = _items.RemoveAll(r => r.name == item.name);
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

        InventoryItem item = GetCurrentItem();
        if (item == null) return false;

        if (interactiveObjectsCollisions != null && interactiveObjectsCollisions.Count > 0)
        {
            IActionReciever interactoveObject = interactiveObjectsCollisions[0].gameObject.GetComponent<IActionReciever>();
            bool success = interactoveObject.Interact(item);
            if (success)
            {
                RemoveItem(item);
                Destroy(item.gameObject);
                MessageInventoryChanged();
            }
            return success;
        }

        // Deploy
        item.gameObject.transform.position = position;
        item.gameObject.SetActive(true);
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
        Messenger<List<InventoryItem>>.Broadcast(GameEvent.INVENTORY_CHANGED, _items);
    }

    private InventoryItem GetCurrentItem()
    {
        if (_items.Count == 0) return null;
        return _items[0];
    }
}
