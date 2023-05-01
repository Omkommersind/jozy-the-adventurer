using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : MonoBehaviour, IActionSuccessHandler
{
    public GameObject obj;
    public bool ActiveSet;
    public void OnSuccess()
    {
        obj.SetActive(ActiveSet);
    }
}
