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
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        navAgent = GetComponent<NavMeshAgent>();

        Node root = new Selector(new List<Node>()
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack()
            }),
            new Sequence(new List<Node> 
            {
                new CheckEnemyInRange(transform),
                new TaskGoToTarget(transform, navAgent)
            }),
            new TaskPatrol(transform, waypoints, navAgent)
    });
        return root;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fovRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
