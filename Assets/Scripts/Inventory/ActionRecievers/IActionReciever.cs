using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IActionReciever
{
    public bool Interact(InventoryItem item);
}
