using BehaviorTree;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CreatureBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;
    private NavMeshAgent navAgent;

    public static float speed = 5f;
    public static float fovRange = 5f;
    public static float fixRange = 1f;

    public float hunger;
    public Transform food;

    protected override Node SetupTree()
    {
        navAgent = GetComponent<NavMeshAgent>();

        // Establises the Behavior Tree and its logic
        Node root = new Selector(new List<Node>()
        {
            new Sequence(new List<Node>
            {
                new CheckHunger(this),
                new TaskGoToTarget(transform, navAgent),
                new TaskEat(this)
            }),
            new Sequence(new List<Node>
            {
                new CheckFixableInFixRange(transform),
                new TaskFix()
            }),
            new Sequence(new List<Node> 
            {
                new CheckFixableInRange(transform),
                new TaskGoToTarget(transform, navAgent)
            }),
            new TaskPatrol(transform, waypoints, navAgent)
        });
        return root;
    }

    protected override void Update()
    {
        base.Update();

        if (hunger > 0)
            hunger -= Time.deltaTime;
    }

    /// <summary>
    /// Refills this Creature's hunger
    /// </summary>
    public void Eat()
    {
        hunger = 20;
    }
    /// <summary>
    /// Get the nearest food source
    /// </summary>
    /// <returns>The closest source of food to this agent</returns>
    public Transform GetClosestFood()
    {
        return food;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fovRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fixRange);
    }
}
