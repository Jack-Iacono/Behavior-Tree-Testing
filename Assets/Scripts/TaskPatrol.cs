using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class TaskPatrol : Node
{
    private Transform transform;
    private Transform[] waypoints;
    private NavMeshAgent navAgent;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitTimer = 0f;
    private bool waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints, NavMeshAgent navAgent)
    {
        this.transform = transform;
        this.waypoints = waypoints;
        this.navAgent = navAgent;
    }
    public override Status Check()
    {
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer < waitTime)
                return Status.RUNNING;
            waiting = false;
        }

        Transform wp = waypoints[currentWaypointIndex];
        if(Vector3.Distance(transform.position, wp.position) < 0.5f)
        {
            waitTimer = 0;
            waiting = true;

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            navAgent.destination = wp.position;
            navAgent.speed = CreatureBT.speed;
        }

        status = Status.RUNNING;
        return status;
    }
}
