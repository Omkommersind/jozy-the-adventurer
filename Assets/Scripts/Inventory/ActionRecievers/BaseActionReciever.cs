using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionReciever : MonoBehaviour, IActionReciever
{
    public InventoryItem RequiredItem;

    public bool Interact(InventoryItem item)
    {
        if (item != null && item == RequiredItem)
        {
            OnSuccess();
            return true;
        }
        return false;
    }

    protected void OnSuccess()
    {
        Destroy(gameObject);
    }
}
