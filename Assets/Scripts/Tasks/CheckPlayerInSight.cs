using BehaviorTree;
using UnityEngine;

public class CheckPlayerInSight : Node
{
    private Transform player;
    private Transform user;

    public CheckPlayerInSight(Transform user, Transform player)
    {
        this.player = player;
        this.user = user;
    }

    public override Status Check()
    {
        // Check if the player is close enough to the user
        if (Vector3.Distance(player.position, user.position) <= CreatureBT.fovRange)
        {
            RaycastHit hit;
            Ray ray = new Ray(user.position, (player.position - user.position).normalized);

            // Check if the player is within the vision arc
            if (Vector3.Dot(user.forward, ray.direction) >= 0.8)
            {
                // Check if the player is behind any walls / obstructions
                if (Physics.Raycast(ray.origin, ray.direction, out hit, CreatureBT.fovRange))
                {
                    if (hit.collider.tag == "Player")
                    {
                        parent.SetData("playerKnownPosition", player.position);

                        status = Status.SUCCESS;
                        return status;
                    }
                }
            }
        }

        // Check if there is still a known position
        if(GetData("playerKnownPosition") != null)
        {
            status = Status.SUCCESS;
            return status;
        }

        // If the enemy can't see the player and there is no known last position, then it is  a failure
        status = Status.FAILURE;
        return status;
    }

}
