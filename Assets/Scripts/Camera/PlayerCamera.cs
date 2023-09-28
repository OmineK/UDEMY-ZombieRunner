using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensivity = 150f;

    Transform orientation;

    public float StartingMouseSensivity { get { return startingMouseSensivity; } }
    float startingMouseSensivity;
    float xRotation;
    float yRotation;

    void Start()
    {
        orientation = FindObjectOfType<Orientation>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startingMouseSensivity = mouseSensivity;
    }

    void Update()
    {
        MouseInput();
    }

    void MouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        CameraRotationProcess();
    }

    void CameraRotationProcess()
    {
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
