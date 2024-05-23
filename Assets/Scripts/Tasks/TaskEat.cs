using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskEat : Node
{
    private CreatureBT owner;

    private float waitTime = 1f;
    private float waitTimer = 1f;

    private bool waiting = false;

    public TaskEat(CreatureBT owner) 
    {
        this.owner = owner;
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

            owner.Eat();

            parent.parent.ClearData("target");

            waiting = false;

            status = Status.SUCCESS;
            return status;
        }
        else
        {
            waitTimer = waitTime;
            waiting = true;

            status = Status.RUNNING;
            return status;
        }
    }
}
