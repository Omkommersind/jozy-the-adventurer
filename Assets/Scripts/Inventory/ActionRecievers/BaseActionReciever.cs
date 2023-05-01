using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionReciever : MonoBehaviour, IActionReciever
{
    public InventoryItem RequiredItem;
    public bool DestroyOnSuccess = true;
    private IActionSuccessHandler[] successHandlers;

    private void Start()
    {
        successHandlers = GetComponents<IActionSuccessHandler>();
    }

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
        foreach (IActionSuccessHandler handler in successHandlers) {
            handler.OnSuccess();
        }
        
        if (DestroyOnSuccess)
        {
            Destroy(gameObject);
        }
    }
}
