using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardDiscription : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI discripstionText;

    public void Set(string cardname , string discripstion)
    {
        nameText.text = cardname;
        discripstionText.text = discripstion;

    }

   
}
