using System;
using UnityEngine;

public abstract class AbstractJoint : MonoBehaviour
{
    [Serializable]
    public struct ConstraintsPerAxis
    {
        public float minAngle;
        public float maxAngle;
    }

    public ConstraintsPerAxis xConstraints;
    public ConstraintsPerAxis yConstraints;
    public ConstraintsPerAxis zConstraints;
    public bool computeIK = true;
    protected AbstractIkOverride Override;

    protected void Awake()
    {
        Override = GetComponent<AbstractIkOverride>();
    }

    private float RemapAngleTo180(float toRemap)
    {
        float angle = toRemap % 360;
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        return angle;
    }

    protected Vector3 ClampAngles(Vector3 toClamp)
    {
        toClamp.x = Mathf.Clamp(RemapAngleTo180(toClamp.x),
            xConstraints.minAngle, xConstraints.maxAngle);
    
        toClamp.y = Mathf.Clamp(RemapAngleTo180(toClamp.y),
            yConstraints.minAngle, yConstraints.maxAngle);
         
        toClamp.z = Mathf.Clamp(RemapAngleTo180(toClamp.z),
            zConstraints.minAngle, zConstraints.maxAngle);

        return toClamp;
    }

    public abstract void ComputeRotation(Transform endEffector, Vector3 target);
}