using UnityEngine;
using System;

[ExecuteInEditMode]
public class PixelPerfectSpriteRenderer : MonoBehaviour
{
    // Todo: rework
    private int pixelsPerUnit = 1;
    private float snapValue;

    // Use this for initialization
    void Start()
    {
        snapValue = 1f / pixelsPerUnit / 16;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Application.isPlaying == false)
            return;

        Vector3 pos = transform.position;
        pos = new Vector3((float)Math.Round(pos.x / snapValue) * snapValue, (float)Math.Round(pos.y / snapValue) * snapValue);
        transform.position = pos;
    }
}