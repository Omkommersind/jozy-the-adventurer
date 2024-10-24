using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Inventory;
using Assets.Scripts.Inventory.ActionRecievers;
using Assets.Scripts.System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxItems = 3;
    private readonly List<ItemData> _items = new List<ItemData>();

    private void Awake()
    {
        Messenger<ItemData>.AddListener(GameEvent.ItemPutToInventory, AddItem);  // Todo: simple method
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RollInventoryRight();
        }
    }

    public List<ItemData> GetItems()
    {
        return _items;
    }

    public void AddItem(ItemData itemData)
    {
        if (_items.Count >= MaxItems) return; // If max items - put last itemView from inventory on ground
        if (_items.Contains(itemData)) return;

        _items.Add(itemData);
        //itemView.gameObject.SetActive(false); Todo
        MessageInventoryChanged();
    }

    public bool RemoveItem(ItemData itemView)
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

        ItemData item = GetCurrentItem();
        if (item == null) return false;

        if (interactiveObjectsCollisions != null && interactiveObjectsCollisions.Count > 0)
        {
            IActionReceiver interactiveObject = interactiveObjectsCollisions[0].gameObject.GetComponent<IActionReceiver>();
            bool success = interactiveObject.Interact(item);
            if (success)
            {
                RemoveItem(item);
                item.Use();
                MessageInventoryChanged();
            }
            return success;
        }

        // Deploy
        item.PutInWorld(position);
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
        Messenger<List<ItemData>>.Broadcast(GameEvent.InventoryChanged, _items);
    }

    private ItemData GetCurrentItem()
    {
        if (_items.Count == 0) return null;
        return _items[0];
    }
}
