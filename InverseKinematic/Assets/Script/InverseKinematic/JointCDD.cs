using UnityEngine;

public class JointCDD : AbstractJoint
{
    public override void ComputeRotation(Transform endEffector, Vector3 target)
    {
        if (!computeIK)
            return;

        Quaternion newQuat;
        if (!Override || !Override.IsOverridingIk || !Override.ComputeRotations(out newQuat))
        {
            //Step 1: Points to target
            Vector3 directionToTip = endEffector.position - transform.position;
            Vector3 directionToTarget = target - transform.position;
            newQuat = Quaternion.FromToRotation(directionToTip, directionToTarget) * transform.rotation;
        }
    
        //Step 2: Constrain Rotation through parent rotation
        Quaternion parentRotation = Quaternion.Inverse(transform.parent.rotation);
        newQuat = parentRotation * newQuat;
    
        //Step 3: Joints Limits
        transform.localRotation = Quaternion.Euler(ClampAngles(newQuat.eulerAngles));
    }
}