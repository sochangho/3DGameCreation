using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InfoCreater : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private CanvasGroup canvasGroup;


    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(LifeRoutin());
    }


    IEnumerator LifeRoutin()
    {
        canvasGroup.alpha = 1;

        while(0 < canvasGroup.alpha)
        {
            canvasGroup.alpha -= 0.006f;

            yield return null;
        }

        Destroy(this.gameObject);
    }

}
