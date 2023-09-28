using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;

    [Header("Object move")]
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    Ammo ammo;
    AudioSource audioSource;

    void Awake()
    {
        ammo = FindObjectOfType<Ammo>();
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
            ammo.AddCurrentAmmo(ammoType, ammoAmount);
            
            audioSource.Play();
            GetComponent<Collider>().enabled = false;

            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

            Destroy(this.gameObject, 2f);
        }
    }
}
