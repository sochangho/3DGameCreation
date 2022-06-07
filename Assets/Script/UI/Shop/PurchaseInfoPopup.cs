using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class PurchaseInfoPopup : MonoBehaviour ,Iinformation
{
    //private ScriptableObject scriptableObject;
    private PurchaseCard purchaseCard;
    private Card card;

    public TextMeshProUGUI priceText;
    public Button purchaseButton;
    public Button exitButton;

    public InfoCreater info;

    public UnityAction closeAction;


    int price;

    


    public void Open(PurchaseCard purchaseCard)
    {
        this.purchaseCard = purchaseCard;
        card = null; 

        if(purchaseCard.data is CharactorData)
        {
            CharactorData charactorData = (CharactorData)purchaseCard.data;
            card = charactorData.charater; 
            price = charactorData.price;
           
        }
        else if(purchaseCard.data is EffectData)
        {
            EffectData effectData = (EffectData)purchaseCard.data;
            card = effectData.effect;
            price = effectData.price;
        }

        priceText.text = price.ToString();

        purchaseButton.onClick.AddListener(Purchase);
        exitButton.onClick.AddListener(Exit);
    }

    private void Purchase()
    {
        int gold = PlayerPrefs.GetInt("gold");
        
        if(card == null)
        {
            Debug.LogError("카드 데이터 x");
            return;
        }


        if(gold < price)
        {

            informationPrint("골드가 부족합니다.");
            return;
        }

        gold -= price;

        PlayerPrefs.SetInt("gold", gold);
        DataAddManager.Instance.DataAdd(card);
        FindObjectOfType<ShopPopup>().SetPlayerGoldText();
        purchaseCard.Blind();
        purchaseCard.button.interactable = false;
        if (closeAction != null)
        {
            closeAction();
        }
        Destroy(this.gameObject);

    }


    private void Exit()
    {
        if (closeAction != null)
        {
            closeAction();
        }

        Destroy(this.gameObject);
    }

   public void informationPrint(string str)
    {
      InfoCreater infoCreater = Instantiate(info);
      var go = FindObjectOfType<Canvas>();
      infoCreater.transform.parent = go.transform;
      infoCreater.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
      infoCreater.infoText.text = str;
    }
}
