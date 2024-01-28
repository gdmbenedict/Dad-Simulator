using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    //public EnemySpeedAdjuster enemySpeedAdjuster;
    public Transform player;
    public RoamState roamState;
    public float chaseDistance = 10.0f;
    [SerializeField]private NavMeshAgent navMeshAgent;

    public void Start()
    {
        //enemySpeedAdjuster = FindObjectOfType<EnemySpeedAdjuster>();
        //enemySpeedAdjuster.ChaseTimer();
    }
    public override State RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseDistance)
        {
            UpdateEnemy();
        }
        else
        {
            navMeshAgent.ResetPath();
            return roamState;
        }
        return this;
    }
    private void UpdateEnemy()
    {
        navMeshAgent.SetDestination(player.position);
    }
}

