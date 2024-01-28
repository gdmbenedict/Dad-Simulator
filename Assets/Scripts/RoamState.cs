using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamState : State
{

    public float chaseDistance = 10.0f;
    public Transform player;
    public ChaseState chaseState;
    [SerializeField] private NavMeshAgent navMeshAgent;
    public override State RunCurrentState()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the player is within the chase distance
            if (distanceToPlayer <= chaseDistance)
            {
                return chaseState;
            }
            else
            {
                UpdateDestination();
                UpdateEnemy();
                return this;
            }
        }
        else
        {
            return this;
        }
    }

    public Transform[] waypoints;
    int minIndex = 0;
    int maxIndex = 7;
    int waypointIndex;
    Vector3 target;
    private void UpdateEnemy()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }
    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        navMeshAgent.SetDestination(target);
    }
    void IterateWaypointIndex()
    {
        int randomIndex = Random.Range(minIndex, maxIndex);
        if (waypointIndex != randomIndex)
        {
            waypointIndex = randomIndex;
        }
    }
}

