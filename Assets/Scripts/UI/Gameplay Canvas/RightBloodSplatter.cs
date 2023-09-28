using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightBloodSplatter : MonoBehaviour
{
    readonly float impactTime = 0.3f;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        image.enabled = false;
    }

    public void ShowRightDamageImpact()
    {
        StartCoroutine(ShowBloodSplatter());
    }

    IEnumerator ShowBloodSplatter()
    {
        image.enabled = true;

        yield return new WaitForSeconds(impactTime);

        image.enabled = false;
    }
}
