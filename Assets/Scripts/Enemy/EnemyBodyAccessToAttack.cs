using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyAccessToAttack : MonoBehaviour
{
#pragma warning disable IDE0051
    EnemyAttack enemyAttack;

    void Awake()
    {
        enemyAttack = GetComponentInParent<EnemyAttack>();
    }


    void EnemyLeftHitEventAccess()

    {
        enemyAttack.LeftAttackHitEvent();
    }

    void EnemyRightHitEventAccess()

    {
        enemyAttack.RightAttackHitEvent();
    }
#pragma warning restore IDE0051
}
