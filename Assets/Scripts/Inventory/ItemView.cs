using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class ItemView : MonoBehaviour
    {
        //public bool IsDefault { get; private set; } = true;
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
    }
}