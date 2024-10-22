using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObjects/Jozy/ItemData", order = 52)]
    public class ItemData : ScriptableObject
    {
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
    }
}
