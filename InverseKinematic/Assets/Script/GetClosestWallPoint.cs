using System;
using UnityEngine;

public class GetClosestWallPoint : MonoBehaviour
{
    [NonSerialized]
    public GameObject Wall;
    private Collider _wallCollider;

    private Vector3 _ikTarget;

    public bool CanBeComputed()
    {
        return Wall;
    }
    
    public bool GetClosestPointOnWall(Vector3 position, out Vector3 outTarget)
    {
        outTarget = Vector3.zero;
        if (!CanBeComputed())
            return false;

        _ikTarget = _wallCollider.ClosestPointOnBounds(position);
        outTarget = _ikTarget;
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Wall"))
            return;
        
        Wall = other.gameObject;
        _wallCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Wall"))
            return;
        
        Wall = null;
        _wallCollider = null;
    }
}
