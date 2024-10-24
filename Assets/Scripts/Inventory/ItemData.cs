using Assets.Scripts.System;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObjects/Jozy/ItemData", order = 52)]
    public class ItemData : ScriptableObject
    {
        [SerializeField]
        private InventoryItemStatusesEnum _status = InventoryItemStatusesEnum.InWorld;
        public InventoryItemStatusesEnum Status => _status;

        [SerializeField]
        private string _name;
        public string Name => _name;

        [SerializeField]
        private bool _exists = true;
        public bool Exists
        {
            get => _exists;
            set => _exists = value;
        }

        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;


        public void Use()
        {
            // Destroy(gameObject);
            _status = InventoryItemStatusesEnum.Redeemed;
            //gameObject.SetActive(false);
        }

        public void PutIntoInventory()
        {
            //if (DefaultData != null)
            //    VirginItems.SetItemState(Data, Scene, false);  Todo: to view

            Messenger<ItemData>.Broadcast(GameEvent.ItemPutToInventory, this);
            _status = InventoryItemStatusesEnum.InInventory;
        }

        public void PutInWorld(Vector3 position)
        {
            //gameObject.transform.position = position;
            //gameObject.SetActive(true); Todo: to view
            _status = InventoryItemStatusesEnum.InWorld;

            GameManager.Instance.UpdateItemData(this);
        }
    }
    public enum InventoryItemStatusesEnum
    {
        InWorld,
        InInventory,
        Redeemed
    }
}
