using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObjects/Jozy/Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private bool _exists;
    public bool Exists
    {
        get => _exists;
        set => _exists = value;
    }


    [SerializeField]
    private Sprite _sprite;
    public string Sprite => _name;
}
