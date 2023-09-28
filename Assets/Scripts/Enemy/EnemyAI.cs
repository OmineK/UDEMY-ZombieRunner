using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float onChaseRangeAggroTime = 15f;
    [SerializeField] float onDamageTakenAggroTime = 5f;
    [SerializeField] float maxAggroTime = 25f;

    bool isProvoked = false;
    float distanceToTarget = Mathf.Infinity;
    float aggroTime = 0f;
    float enemySpeed;

    Animator enemyAnim;
    NavMeshAgent navMeshAgent;
    PlayerHealth target;

    void Awake()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        CurrentEnemySpeed();
        DistanceCalculate();
        EnemyProvocation();
        AggroTimeCalculator();
    }

    void CurrentEnemySpeed()
    {
        enemySpeed = navMeshAgent.velocity.magnitude;
    }

    void DistanceCalculate()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
    }

    void EnemyProvocation()
    {
        if (target.CurrentPlayerHealth > 0)
        {
            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                aggroTime += onChaseRangeAggroTime;
                isProvoked = true;
            }
            else
            {
                enemyAnim.SetBool("attack", false);
                enemyAnim.SetTrigger("idle");
            }
        }
        else
        {
            isProvoked = false;
            aggroTime = 0f;
            enemyAnim.SetBool("attack", false);
            enemyAnim.SetTrigger("idle");
        }
    }

    //How long this game Object (enemy) have to track the target Object (player)
    void AggroTimeCalculator()
    {
        if (aggroTime <= 0)
        {
            isProvoked = false;
            navMeshAgent.isStopped = true;
        }
        else
        {
            aggroTime -= Time.deltaTime;
        }

        if (aggroTime >= maxAggroTime)
        {
            aggroTime = maxAggroTime;
        }
    }

    void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void ChaseTarget()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.transform.position);

        enemyAnim.SetBool("attack", false);
       

        if (enemySpeed < 1f)
        {
            enemyAnim.SetTrigger("walk");
        }
        else
        {
            enemyAnim.SetTrigger("run");
        }
    }

    void AttackTarget()
    {
        enemyAnim.SetBool("attack", true);
    }

    //BroadcastMessage - EnemyHealth (script)
    public void OnDamageTaken()
    {
        aggroTime += onDamageTakenAggroTime;
        isProvoked = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
