using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckArea : Node
{
    private float waitTime = 1f;
    private float waitTimer = 1f;

    private bool waiting = false;

    private Transform player;
    private Transform user;

    public CheckArea(Transform user, Transform player)
    {
        this.player = player;
        this.user = user;
    }

    public override Status Check()
    {
        if (waiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer > 0)
            {
                status = Status.RUNNING;
                return status;
            }

            waiting = false;

            ClearData("playerKnownPosition");

            status = Status.SUCCESS;
            return status;
        }
        else
        {
            // Check if the player is close enough to the user
            if (Vector3.Distance(player.position, user.position) <= CreatureBT.fovRange)
            {
                RaycastHit hit;
                Ray ray = new Ray(user.position, (player.position - user.position).normalized);

                Debug.Log(Vector3.Dot(user.forward, ray.direction));

                // Check if the player is within the vision arc
                if (Vector3.Dot(user.forward, ray.direction) >= 0.1)
                {
                    // Check if the player is behind any walls / obstructions
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, CreatureBT.fovRange))
                    {
                        if (hit.collider.tag == "Player")
                        {
                            waiting = false;
                            waitTimer = waitTime;

                            parent.SetData("playerKnownPosition", player.position);

                            status = Status.SUCCESS;
                            return status;
                        }
                    }
                }
            }

            waitTimer = waitTime;
            waiting = true;

            status = Status.RUNNING;
            return status;
        }
    }
}
