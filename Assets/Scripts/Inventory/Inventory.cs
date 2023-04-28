using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxItems = 3;
    private readonly List<InventoryItem> _items = new List<InventoryItem>();

    private void Awake()
    {
        Messenger<InventoryItem>.AddListener(GameEvent.ITEM_PUT_TO_INVENTORY, AddItem);
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
        Messenger<List<InventoryItem>>.Broadcast(GameEvent.INVENTORY_CHANGED, _items);
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
