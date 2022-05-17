using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CostGage : MonoBehaviour
{
    public Text costText;
    public Image gage;
   
    public void FillAmountGage(float costRatio)
    {
        gage.fillAmount = costRatio;
        
    }

    public void SetCost(int cost)
    {

        costText.text = cost.ToString();
    }





}
