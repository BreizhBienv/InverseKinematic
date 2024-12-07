using System;
using UnityEngine;

public abstract class AbstractIkOverride : MonoBehaviour
{
    protected ComputeIk ParentComputeIk;
    
    [NonSerialized] public bool IsOverridingIk = false;

    protected void Awake()
    {
        ParentComputeIk = GetComponentInParent<ComputeIk>();
    }

    public abstract bool ComputeRotations(out Quaternion outQuat);
}
