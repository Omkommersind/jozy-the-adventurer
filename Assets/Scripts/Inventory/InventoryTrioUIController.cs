using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTrioUIController : MonoBehaviour
{
    public Image Item1;
    public Image Item2;
    public Image Item3;

    private Image[] _images = new Image[3];

    private void Awake()
    {
        Messenger<List<InventoryItem>>.AddListener(GameEvent.INVENTORY_CHANGED, UpdateImages);
    }

    private void Start()
    {
        _images[0] = Item3;
        _images[1] = Item2;
        _images[2] = Item1;
    }

    void UpdateImages(List<InventoryItem> items)
    {
        for (var i = 0; i < items.Count; i++)
        {
            _images[i].sprite = items[i].Sprite;
            _images[i].color = new Color(1, 1, 1, 1);
        }

        for (var i = 2; i >= items.Count; i--)
        {
            _images[i].sprite = null;
            _images[i].color = new Color(1, 1, 1, 0);
        }
    }
}
