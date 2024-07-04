using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameManager GameManager;

    void Awake()
    {
        if (GameManager.Instance == null)
            Instantiate(GameManager);
    }
}
