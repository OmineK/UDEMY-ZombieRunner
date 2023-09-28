using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [Header("Battery options")]
    [SerializeField] float restoreAngle;
    [SerializeField] float restoreIntensityAmount = 1f;

    [Header("Object move")]
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    Flashlight flashlight;
    AudioSource audioSource;

    void Awake()
    {
        flashlight = FindObjectOfType<Flashlight>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        ObjectMove();
    }

    void ObjectMove()
    {
        if (period == Mathf.Epsilon) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        float movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (flashlight.GetComponent<Light>().intensity >= flashlight.MaximumIntensity - 0.5f) { return; }

            audioSource.Play();
            GetComponent<Collider>().enabled = false;

            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

            flashlight.RestoreLight(restoreIntensityAmount);
            Destroy(this.gameObject, 2f);
        }
    }
}
