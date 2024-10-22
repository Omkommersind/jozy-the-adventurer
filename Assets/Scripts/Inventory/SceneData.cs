using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New SceneData", menuName = "ScriptableObjects/Jozy/SceneData", order = 51)]
    public class SceneData : ScriptableObject
    {
        [SerializeField]
        private List<ItemData> _items;
        public List<ItemData> Items => _items;

        public void AddItem(ItemData item)
        {
            // Todo check exists
            _items.Add(item);
        }
    }
}
