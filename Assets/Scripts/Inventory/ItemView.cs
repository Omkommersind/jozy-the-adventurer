using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class ItemView : MonoBehaviour
    {
        //public bool IsDefault { get; private set; } = true;
        private InventoryItemStatusesEnum _status = InventoryItemStatusesEnum.InWorld;
        public ItemData Data;
        public ItemData DefaultData;
        public ItemsOnScenes VirginItems;
        public SceneData Scene;

        private void Awake()
        {
            if (DefaultData != null)
            {
                var shouldInit = VirginItems.GetItemState(DefaultData, Scene);
                if (shouldInit)
                    LoadData(DefaultData);
                else
                    Destroy(gameObject);
            }
        }

        private void LoadData(ItemData data)
        {
            Data = data;
            GetComponent<SpriteRenderer>().sprite = Data.Sprite;
        }

        public void PutIntoInventory()
        {
            if (DefaultData != null)
                VirginItems.SetItemState(Data, Scene, false);

            Messenger<ItemView>.Broadcast(GameEvent.ITEM_PUT_TO_INVENTORY, this);
            _status = InventoryItemStatusesEnum.InInventory;
        }

        public void PutInWorld(Vector3 position)
        {
            gameObject.transform.position = position;
            gameObject.SetActive(true);
            _status = InventoryItemStatusesEnum.InWorld;

            GameManager.Instance.UpdateItemData(this);
        }

        public void Use()
        {
            // Destroy(gameObject);
            _status = InventoryItemStatusesEnum.Redeemed;
            gameObject.SetActive(false);
        }
    }

    public enum InventoryItemStatusesEnum
    {
        InWorld,
        InInventory,
        Redeemed
    }
}