using Assets.Scripts.Inventory;

interface IActionReceiver
{
    public bool Interact();
    public bool Interact(ItemView itemView);
}