using BehaviorTree;
using UnityEngine;

public class CheckEnemyInRange : Node
{
    private Transform transform;
    private static int enemyLayerMask = 1 << 6;

    public CheckEnemyInRange(Transform transform)
    {
        this.transform = transform;
    }

    public override Status Check()
    {
        object t = GetData("target");

        // Check if there is data already stored for the target within this roots of this branch
        if (t == null)
        {
            // Get everything within the a given area around the player
            Collider[] colliders = Physics.OverlapSphere(transform.position, CreatureBT.fovRange, enemyLayerMask);

            // If there is something that matches what we are looking for, set it as the new target
            if(colliders.Length > 0)
            {
                // Sets the data for target
                parent.parent.SetData("target", colliders[0].transform);

                status = Status.SUCCESS;
                return status;
            }

            status = Status.FAILURE;
            return status;
        }

        // If there is data, then this action is a success
        status = Status.SUCCESS;
        return status;
    }
}
