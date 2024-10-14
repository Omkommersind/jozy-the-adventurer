using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemsOnScenes", order = 1)]
public class ItemsOnScenes : ScriptableObject
{
    public List<SceneEntry> SceneEntries;
    public Dictionary<SceneSO, SceneEntry> Scenes = new Dictionary<SceneSO, SceneEntry>();

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

    public bool GetItemState(ItemSO item, SceneSO scene)
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

    public void SetItemState(ItemSO item, SceneSO scene, bool value)
    {
        Scenes[scene].Items[item].Value = value;
    }
}

[Serializable]
public class SceneEntry
{
    [HideInInspector]
    public string Name;
    public SceneSO Scene;
    public List<ItemEntry> ItemEntries = new List<ItemEntry>();
    public Dictionary<ItemSO, ItemEntry> Items = new Dictionary<ItemSO, ItemEntry>();
}

[Serializable]
public class ItemEntry
{
    [SerializeField, HideInInspector]
    private string _name;
    public ItemSO Item;
    public bool Value = true;

    public ItemEntry(ItemSO item)
    {
        _name = item.name;
        Item = item;
    }
}