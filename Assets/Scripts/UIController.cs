using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text velocityLabel;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnVelocityChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnVelocityChanged);
    }

    private void OnVelocityChanged(float value)
    {
        if (velocityLabel != null)
        {
            velocityLabel.text = value.ToString();
        }
    }

    private void OnDrawGizmos()
    {
        // Todo
    }
}
