using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Dictionary<string, List<ItemData>> ItemsData;
    private string _activeScene;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {

    }

    public void UpdateItemData(ItemData item)
    {
        var scene = SceneManager.GetActiveScene().name;
        var index = ItemsData[scene].FindIndex(i => i.Name == item.Name);
        if (index != -1)
            ItemsData[scene][index] = item;
        else
            ItemsData[scene].Add(item);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
        _activeScene = sceneName;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
