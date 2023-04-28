using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string Name;
    public Sprite Sprite;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void PutIntoInventory()
    {
        Messenger<InventoryItem>.Broadcast(GameEvent.ITEM_PUT_TO_INVENTORY, this);
    }
}
