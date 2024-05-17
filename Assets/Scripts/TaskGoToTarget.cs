using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private Transform transform;
    private NavMeshAgent navAgent;

    public TaskGoToTarget(Transform transform, NavMeshAgent navAgent)
    { 
        this.transform = transform; 
        this.navAgent = navAgent;
    }

    public override Status Check()
    {
        Transform target = (Transform)GetData("target");

        if(Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            navAgent.destination = target.position;
        }

        status = Status.RUNNING;
        return status;
    }

}
