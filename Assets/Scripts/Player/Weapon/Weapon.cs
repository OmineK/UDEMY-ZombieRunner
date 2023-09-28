using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] int damageAmount = 25;
    [SerializeField] float timeBetweenShoots = .5f;
    [SerializeField] AmmoType ammoType;

    [SerializeField] ParticleSystem shootFlashVFX;
    [SerializeField] GameObject hitVFX;

    AudioSource audioSource;
    Camera playerCamera;
    Ammo ammoSlot;
    AmmoText ammoText;

    bool canShoot = true;

    void OnEnable()
    {
        canShoot = true;
    }

    void Awake()
    {
        playerCamera = FindObjectOfType<Camera>();
        ammoSlot = GetComponentInParent<Ammo>();
        ammoText = FindObjectOfType<AmmoText>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        AmmoTextUpdate();
        StartCoroutine(Shoot());
    }

    void AmmoTextUpdate()
    {
        ammoText.gameObject.GetComponent<TextMeshProUGUI>().text = $"Ammo: {ammoSlot.GetCurrentAmmo(ammoType)}";
    }

    IEnumerator Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) &&
            ammoSlot.GetCurrentAmmo(ammoType) > 0 &&
            canShoot == true)
        {
            canShoot = false;

            audioSource.Play();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayShootVFX();
            RaycastProcess();

            yield return new WaitForSeconds(timeBetweenShoots);
            canShoot = true;
        }
    }

    void PlayShootVFX()
    {
        shootFlashVFX.Play();
    }

    void RaycastProcess()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,
            out RaycastHit hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponentInParent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(damageAmount);

        }
        else
        {
            return;
        }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}
