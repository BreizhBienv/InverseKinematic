using UnityEngine;

public class IkHandOverride : AbstractIkOverride
{
    private SphereCollider _sphereCollider;

    protected new void Awake()
    {
        base.Awake();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    public override bool ComputeRotations(out Quaternion outQuat)
    {
        outQuat = Quaternion.identity;
        
        Vector3 traceStart = transform.position;
        Vector3 traceEnd = ParentComputeIk.TargetLocation;

        if (!Physics.Raycast(traceStart, traceEnd - traceStart, out var hit, _sphereCollider.radius * 2f))
            return false;
        
        Quaternion newQuat = Quaternion.FromToRotation(transform.forward, hit.normal * -1f) * transform.rotation;
        newQuat = Quaternion.FromToRotation(newQuat * Vector3.up, Vector3.up) * newQuat;
        outQuat = newQuat;
        return true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Wall"))
            return;
        
        IsOverridingIk = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Wall"))
            return;
        
        IsOverridingIk = false;
    }
}
