using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAttack : Node
{
    public TaskAttack() { }

    public override Status Check()
    {
        Transform target = (Transform)GetData("target");

        Debug.Log("Attack");
        status = Status.SUCCESS;
        return status;
    }

}
