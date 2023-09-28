using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 100;
    [SerializeField] AudioClip zombieDeathSFX;

    bool isDead = false;

    Animator enemyAnim;
    AudioSource zombieAudio;

    void Awake()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        zombieAudio = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        BroadcastMessage("OnDamageTaken");

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) { return; }

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;

        zombieAudio.PlayOneShot(zombieDeathSFX);

        foreach (Transform child in transform)
            child.gameObject.GetComponent<AudioSource>().enabled = false;

        isDead = true;

        enemyAnim.SetTrigger("death");
        Destroy(this.gameObject, 4f);
    }
}
