namespace Assets.Scripts.Inventory.ActionRecievers
{
    interface IActionReceiver
    {
        public bool Interact();
        public bool Interact(ItemData item);
    }
}