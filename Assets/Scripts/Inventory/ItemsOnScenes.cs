using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemsOnScenes", menuName = "ScriptableObjects/Jozy/ItemsOnScenes", order = 53)]
    public class ItemsOnScenes : ScriptableObject
    {
        public List<SceneEntry> SceneEntries;
        public Dictionary<SceneData, SceneEntry> Scenes = new Dictionary<SceneData, SceneEntry>();

        void OnEnable()
        {
            Assert.IsFalse(Scenes.Count > 0, "Dictionary is not empty");

            foreach (var sceneEntry in SceneEntries)
            {
                Scenes[sceneEntry.Scene] = sceneEntry;
                foreach (var itemEntry in sceneEntry.ItemEntries)
                {
                    Scenes[sceneEntry.Scene].Items[itemEntry.Item] = itemEntry;
                }
            }
        }

        public bool GetItemState(ItemData item, SceneData scene)
        {
            if (!Scenes.ContainsKey(scene))
            {
                SceneEntry newEntry = new SceneEntry() {Scene = scene, Name = scene.name};
                Scenes[scene] = newEntry;
                SceneEntries.Add(newEntry);
            }

            if (!Scenes[scene].Items.ContainsKey(item))
            {
                ItemEntry newEntry = new ItemEntry(item);
                Scenes[scene].Items[item] = newEntry;
                Scenes[scene].ItemEntries.Add(newEntry);
            }

            return Scenes[scene].Items[item].Value;
        }

        public void SetItemState(ItemData item, SceneData scene, bool value)
        {
            Scenes[scene].Items[item].Value = value;
        }
    }

    [Serializable]
    public class SceneEntry
    {
        [HideInInspector]
        public string Name;
        public SceneData Scene;
        public List<ItemEntry> ItemEntries = new List<ItemEntry>();
        public Dictionary<ItemData, ItemEntry> Items = new Dictionary<ItemData, ItemEntry>();
    }

    [Serializable]
    public class ItemEntry
    {
        [SerializeField, HideInInspector]
        private string _name;
        public ItemData Item;
        public bool Value = true;

        public ItemEntry(ItemData item)
        {
            _name = item.name;
            Item = item;
        }
    }
}