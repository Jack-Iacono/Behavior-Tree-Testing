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
        // check if the agent is waiting
        if (waiting)
        {
            // Increment the timer
            waitTimer += Time.deltaTime;

            // Check if the timer is still above 0
            if (waitTimer < waitTime)
                return Status.RUNNING;

            // Set the agent to stop waiting
            waiting = false;
        }

        // Get the waypoint that the agent should go to
        Transform wp = waypoints[currentWaypointIndex];

        // Check if the agent is not at the destination yet
        if(Vector3.Distance(transform.position, wp.position) < 0.5f)
        {
            waitTimer = 0;
            waiting = true;

            // Switch to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            // Make the agent go to the waypoint
            navAgent.destination = wp.position;
            navAgent.speed = CreatureBT.speed;
        }

        status = Status.RUNNING;
        return status;
    }
}
