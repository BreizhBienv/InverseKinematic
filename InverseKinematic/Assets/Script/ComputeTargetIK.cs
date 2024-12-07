using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class ComputeTargetIK : MonoBehaviour
{
    [SerializeField] private bool activateIK = true;

    [SerializeField] private Transform body;
    
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform rightShoulder;

    [FormerlySerializedAs("leftIK")] [SerializeField] private ComputeIk leftComputeIk;
    [FormerlySerializedAs("rightIK")] [SerializeField] private ComputeIk rightComputeIk;
    
    private GetClosestWallPoint _getPointOnWallScript;
    
    // Start is called before the first frame update
    void Start()
    {
        _getPointOnWallScript = GetComponentInChildren<GetClosestWallPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateIK)
            return;

        if (!_getPointOnWallScript || !_getPointOnWallScript.CanBeComputed())
        {
            leftComputeIk.Activated = false;
            rightComputeIk.Activated = false;
            return;
        }

        _getPointOnWallScript.GetClosestPointOnWall(transform.position, out Vector3 pointOnWall);
        Vector3 playerWallDir = pointOnWall - transform.position;

        if (Vector3.Dot(body.transform.forward, playerWallDir) < -0.3f)
        {
            leftComputeIk.Activated = false;
            rightComputeIk.Activated = false;
            return;
        }
        
        float dotDir = Vector3.Dot(body.transform.right, playerWallDir);

        if (dotDir < 0) //left arm
        {
            leftComputeIk.Activated = true;
            Vector3 origin = leftShoulder.position;
            _getPointOnWallScript.GetClosestPointOnWall(origin, out Vector3 targetLeftShoulderIK);
            leftComputeIk.TargetLocation = targetLeftShoulderIK;
        }
        else
            leftComputeIk.Activated = false;

        if (dotDir > 0) //right arm
        {
            rightComputeIk.Activated = true;
            Vector3 origin = rightShoulder.position;
            _getPointOnWallScript.GetClosestPointOnWall(origin, out Vector3 targetRightShoulderIK);
            rightComputeIk.TargetLocation = targetRightShoulderIK;
        }
        else
            rightComputeIk.Activated = false;
    }
}
