using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    FinishGameCanvas finishGameCanvas;
    WeaponSwitcher weapon;

    void Awake()
    {
        finishGameCanvas = FindObjectOfType<FinishGameCanvas>();
        weapon = FindObjectOfType<WeaponSwitcher>();
    }

    void Start()
    {
        finishGameCanvas.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            weapon.gameObject.SetActive(false);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            finishGameCanvas.gameObject.SetActive(true);
        }
    }
}
