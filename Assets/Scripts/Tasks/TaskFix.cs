using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Unity.VisualScripting;

public class TaskFix : Node
{
    private float waitTime = 1f;
    private float waitTimer = 1f;

    private bool waiting = false;

    public TaskFix() { }

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

            Transform target = (Transform)GetData("target");

            target.gameObject.GetComponent<FixableController>().Fix();

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
