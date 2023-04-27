using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string Name;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void PutIntoInventory()
    {
        Messenger<InventoryItem>.Broadcast(GameEvent.ITEM_PUT_TO_INVENTORY, this);
    }
}
