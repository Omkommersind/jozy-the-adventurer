using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxItems = 3;
    private readonly List<InventoryItem> _items = new List<InventoryItem>();

    public List<InventoryItem> GetItems()
    {
        return _items;
    }

    public bool AddItem(InventoryItem item)
    {
        if (_items.Count >= MaxItems) return false;
        if (_items.Contains(item)) return false;

        _items.Add(item);
        Messenger<List<InventoryItem>>.Broadcast(GameEvent.INVENTORY_CHANGED, _items);
        return true;
    }

    public bool RemoveItem(InventoryItem item)
    {
        int deleted = _items.RemoveAll(r => r.name == item.name);
        if (deleted > 0)
        {
            Messenger<List<InventoryItem>>.Broadcast(GameEvent.INVENTORY_CHANGED, _items);
            return true;
        }
        return false;
    }
}
