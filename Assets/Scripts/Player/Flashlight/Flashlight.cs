using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]float intensityDecay = 0.05f;
    [SerializeField] float angleDecay = 0.4f;
    [SerializeField] float minimumIntensityDecay = 0.5f;

    public float MaximumIntensity { get { return startingIntensity; } }
    float startingIntensity;
    float maximumAngle;

    Light flashlight;

    void Awake()
    {
        flashlight = GetComponent<Light>();
    }

    void Start()
    {
        startingIntensity = flashlight.intensity;
        maximumAngle = flashlight.spotAngle;
    }

    void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    void DecreaseLightAngle()
    {
        if (flashlight.intensity <= 0f) { return; }

        flashlight.spotAngle -= angleDecay * Time.deltaTime;
    }

    void DecreaseLightIntensity()
    {
        if (flashlight.intensity <= 0f) { return; }

        flashlight.intensity -= intensityDecay * Time.deltaTime;

        if (flashlight.intensity <= minimumIntensityDecay)
        {
            flashlight.intensity = 0f;
        }
    }

    public void RestoreLight(float restoreIntensityAmount)
    {
        if (flashlight.intensity >= startingIntensity - 0.5f) { return; }

        flashlight.intensity += restoreIntensityAmount;
        flashlight.spotAngle = maximumAngle;

        if (flashlight.intensity >= startingIntensity)
        {
            flashlight.intensity = startingIntensity;
        }
    }
}
