using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damage = 20;

    [SerializeField] AudioClip zombieAttackSFX;

    PlayerHealth targetToAttack;
    LeftBloodSplatter leftImpact;
    RightBloodSplatter rightImpact;
    AudioSource zombieAudio;
    

    void Awake()
    {
        targetToAttack = FindObjectOfType<PlayerHealth>();
        leftImpact = FindObjectOfType<LeftBloodSplatter>();
        rightImpact = FindObjectOfType<RightBloodSplatter>();
        zombieAudio = GetComponent<AudioSource>();
    }

    public void LeftAttackHitEvent()
    {
        if (targetToAttack == null) { return; }
        leftImpact.ShowLeftDamageImpact();
        zombieAudio.PlayOneShot(zombieAttackSFX);
        targetToAttack.DecresePlayerHealth(damage);
    }

    public void RightAttackHitEvent()
    {
        if (targetToAttack == null) { return; }
        rightImpact.ShowRightDamageImpact();
        zombieAudio.PlayOneShot(zombieAttackSFX);
        targetToAttack.DecresePlayerHealth(damage);
    }
}
