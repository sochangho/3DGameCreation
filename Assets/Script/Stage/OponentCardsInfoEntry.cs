using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OponentCardsInfoEntry : MonoBehaviour
{
    [SerializeField]
    private Image cardImage;

    [SerializeField]
    private TextMeshProUGUI cardnameTex;

    public void CardInfoSet(Sprite cardsprite , string cardname)
    {
        cardImage.sprite = cardsprite;
        cardnameTex.text = cardname;
    }



}
