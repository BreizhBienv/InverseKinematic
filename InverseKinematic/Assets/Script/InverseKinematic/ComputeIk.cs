using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class ComputeIk : MonoBehaviour
{
    [FormerlySerializedAs("limbTip")]
    [Header("Mandatory")]
    [SerializeField] private Transform endEffector;

    [Header("Target")] 
    [SerializeField] private GameObject targetOverride;
    
    [NonSerialized] public bool Activated = false;
    [NonSerialized] public Vector3 TargetLocation;
    private AbstractJoint[] _joints;

    private bool _forceActivation = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (endEffector is null)
        {
            Debug.LogError("limbTip is null - Aborting computing");
            return;
        }

        _joints = GetComponentsInChildren<AbstractJoint>();
        _forceActivation = targetOverride is not null;
    }

    private void FixedUpdate()
    {
        if (!Activated && !targetOverride)
            return;
        
        for (int i = _joints.Length - 1; i >= 0; --i)
            _joints[i].ComputeRotation(endEffector, targetOverride ? targetOverride.transform.position : TargetLocation);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!Activated || targetOverride)
            return;
        
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(TargetLocation, 0.1f);
    }
}
