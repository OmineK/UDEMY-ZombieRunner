using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maximumHealthPoint = 100;

    public int CurrentPlayerHealth { get { return currentPlayerHealth; } }

    int currentPlayerHealth;

    GameObject gameOverCanvas;

    void Awake()
    {
        currentPlayerHealth = maximumHealthPoint;
    }

    void Start()
    {
        gameOverCanvas = FindObjectOfType<GameOverCanvas>().gameObject;
        gameOverCanvas.SetActive(false);
    }

    public void DecresePlayerHealth(int damage)
    {
        currentPlayerHealth -= damage;

        if (currentPlayerHealth <= 0)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        GetComponentInChildren<Weapon>().gameObject.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverCanvas.SetActive(true);
    }

    public void IncresePlayerHealth(int heal)
    {
        currentPlayerHealth += heal;

        if (currentPlayerHealth >= maximumHealthPoint)
        {
            currentPlayerHealth = maximumHealthPoint;
        }
    }
}
