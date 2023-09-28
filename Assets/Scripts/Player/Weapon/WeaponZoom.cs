using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float zoomedIn = 30f;
    [SerializeField] float zoomedOut;
    [SerializeField] float zoomedMouseSensivity = 100f;

    Camera FPPcamera;
    PlayerCamera playerCamera;

    void Awake()
    {
        FPPcamera = GetComponentInParent<Camera>();
        playerCamera = GetComponentInParent<PlayerCamera>();
        zoomedOut = FPPcamera.fieldOfView;
    }

    void Update()
    {
        ZoomWeapon();
        ChangeMouseSensivity();
    }

    void OnDisable()
    {
            FPPcamera.fieldOfView = zoomedOut;
            playerCamera.mouseSensivity = playerCamera.StartingMouseSensivity;
    }

    void ZoomWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (FPPcamera.fieldOfView == zoomedOut)
            {
                FPPcamera.fieldOfView = zoomedIn;
            }
            else if (FPPcamera.fieldOfView == zoomedIn)
            {
                FPPcamera.fieldOfView = zoomedOut;
            }
        }
    }

    void ChangeMouseSensivity()
    {
        if (FPPcamera.fieldOfView == zoomedIn)
        {
            playerCamera.mouseSensivity = zoomedMouseSensivity;
        }
        else
        {
            playerCamera.mouseSensivity = playerCamera.StartingMouseSensivity;
        }
    }
}
