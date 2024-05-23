using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskChasePlayer : Node
{
    private Transform player;
    private NavMeshAgent navAgent;
    private Transform transform;

    public TaskChasePlayer(Transform transform, NavMeshAgent navAgent)
    {
        player = PlayerController.Instance.transform;
        this.transform = transform;
        this.navAgent = navAgent;
    }

    public override Status Check()
    {
        // Get the current target node
        Vector3 target = (Vector3)GetData("playerKnownPosition");

        // Check if the agent is still not at the target
        if (Vector3.Distance(transform.position, target) > 0.5f)
        {
            navAgent.destination = target;
            status = Status.RUNNING;
            return status;
        }

        status = Status.SUCCESS;
        return status;
    }
}
