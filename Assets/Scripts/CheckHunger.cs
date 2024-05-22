using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckHunger : Node
{
    private CreatureBT owner;

    public CheckHunger(CreatureBT owner) : base()
    {
        this.owner = owner;
    }

    public override Status Check()
    {
        // Check if the owner of this node is hungry
        if(owner.hunger <= 0)
        {
            // Set the agent's target to the closest food source
            parent.parent.SetData("target", owner.GetClosestFood());

            status = Status.SUCCESS;
            return status;
        }

        status = Status.FAILURE;
        return status;
    }

}
