using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpText : MonoBehaviour
{
    PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        HpTextUpdate();
    }

    void HpTextUpdate()
    {
        GetComponent<TextMeshProUGUI>().text = $"HP: {playerHealth.CurrentPlayerHealth}";
    }
}
