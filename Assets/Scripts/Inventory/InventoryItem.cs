using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UIElements;

public class InventoryItem : MonoBehaviour
{
    public string Name;
    public Sprite Sprite;
    private InventoryItemStatusesEnum _status = InventoryItemStatusesEnum.InWorld;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void PutIntoInventory()
    {
        Messenger<InventoryItem>.Broadcast(GameEvent.ITEM_PUT_TO_INVENTORY, this);
        _status = InventoryItemStatusesEnum.InInventory;
    }

    public void PutInWorld(Vector3 position)
    {
        gameObject.transform.position = position;
        gameObject.SetActive(true);
        _status = InventoryItemStatusesEnum.InWorld;
    }

    public void Use()
    {
        // Destroy(gameObject);
        _status = InventoryItemStatusesEnum.Used;
        gameObject.SetActive(false);
    }
}

public enum InventoryItemStatusesEnum
{
    InWorld,
    InInventory,
    Used
}