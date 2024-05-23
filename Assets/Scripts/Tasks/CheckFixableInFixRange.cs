using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckFixableInFixRange : Node
{
    public static int fixLayerMask = 1 << 6;

    private Transform transform;

    public CheckFixableInFixRange(Transform transform)
    {
        this.transform = transform;
    }

    public override Status Check()
    {
        object t = GetData("target");

        // Check if there is data already stored for the target within this roots of this branch
        if (t == null)
        {
            status = Status.FAILURE;
            return status;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) < CreatureBT.fixRange)
        {
            status = Status.SUCCESS;
            return status;
        }

        // If there is data, then this action is a success
        status = Status.FAILURE;
        return status;
    }
}
