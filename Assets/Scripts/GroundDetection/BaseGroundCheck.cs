using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGroundCheck : MonoBehaviour
{
    public virtual bool IsGrounded
    {
        get;
    }
}