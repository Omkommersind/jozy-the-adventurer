using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionReceiver : MonoBehaviour, IActionReceiver
{
    public ItemView RequiredItemView;
    public bool DestroyOnSuccess = true;
    private IActionSuccessHandler[] successHandlers;

    private void Start()
    {
        successHandlers = GetComponents<IActionSuccessHandler>();
    }

    public bool Interact()
    {
        OnSuccess();
        return true;
    }


    public bool Interact(ItemView itemView)
    {
        if (itemView == null || itemView == RequiredItemView)
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
