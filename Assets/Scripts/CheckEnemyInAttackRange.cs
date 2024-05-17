using BehaviorTree;
using UnityEngine;

public class CheckEnemyInAttackRange : Node
{
    public static int enemyLayerMask = 1 << 6;

    private Transform transform;

    public CheckEnemyInAttackRange(Transform transform) 
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
        if(Vector3.Distance(transform.position, target.position) < CreatureBT.attackRange)
        {
            status = Status.SUCCESS;
            return status;
        }

        // If there is data, then this action is a success
        status = Status.FAILURE;
        return status;
    }
}
