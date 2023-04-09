using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGroundCheck : MonoBehaviour
{
    public float CheckHeight = 0.1f;
    public virtual bool IsGrounded
    {
        get;
    }
}