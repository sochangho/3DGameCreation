using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseCard : CardElement
{
    public GameObject blind;



   public void Blind()
   {
        blind.SetActive(true);

   }


    protected override void CreateInfo()
    {
        GameObject info = null;
        if (data is CharactorData)
        {
            info = Instantiate(infoObject);

        }
        else if (data is EffectData)
        {

            info = Instantiate(effectinfoObject);
        }


        info.transform.parent = FindObjectOfType<Canvas>().transform;
        info.GetComponent<RectTransform>().position = Input.mousePosition;
        infoObjectClone = info;


        var information = infoObjectClone.GetComponent<CardDiscription>();
        information.Set(data);
    }
}
