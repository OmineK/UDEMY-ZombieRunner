using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 10;
    [SerializeField] float rotateSpeed = 5f;

    PlayerHealth playerHealth;
    AudioSource audioSource;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Spin();
    }

    void Spin()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime * 10f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerHealth.CurrentPlayerHealth < 100)
        {
            playerHealth.IncresePlayerHealth(healAmount);

            audioSource.Play();
            GetComponent<Collider>().enabled = false;

            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

            Destroy(this.gameObject, 2f);
        }
    }
}
