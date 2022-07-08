using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardBuild : MonoBehaviour
{
    public CardInfo info;
    public List<Transform> cardFixTransform;
    public NextCard next;


    public void CardBuildInit()
    {
        var handCards  = GameSceneManager.Instance.ownPlayer.handCardDatas;

          
        for (int i = 0; i < handCards.Count; i++)
        {
            Player.ObjectBundle objectBundle = handCards[i];
            object data = objectBundle.obj;

            CardInfo cardInfo  = Instantiate(info);
            
           
            if(FindEmptyChild() == null)
            {
                return;
            }

            cardInfo.transform.parent = FindEmptyChild();

            cardInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            cardInfo.GetComponent<RectTransform>().localScale = Vector3.one;
            cardInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(cardInfo.transform.parent.GetComponent<RectTransform>().sizeDelta.x,
                cardInfo.transform.parent.GetComponent<RectTransform>().sizeDelta.y);
            cardInfo.id = objectBundle.id;

            if (data is CharactorData)
            {
                CharactorData charactorData = (CharactorData)data;

                cardInfo.icon.sprite = charactorData.sprite;
                cardInfo.cost = charactorData.cost;
                cardInfo.costText.text = cardInfo.cost.ToString();
                cardInfo.card = charactorData.charater;
            }
            else if(data is EffectData)
            {
                EffectData effectData = (EffectData)data;

                cardInfo.icon.sprite = effectData.sprite;
                cardInfo.cost = effectData.cost;
                cardInfo.costText.text = cardInfo.cost.ToString();
                cardInfo.card = effectData.effect;

            }

        }
    }



   public void CardInsert(Player.ObjectBundle objectBundle)
   {

        CardInfo newCardinfo = Instantiate(info);

        if (FindEmptyChild() == null)
        {
            return;
        }

        ScriptableObject data = (ScriptableObject)objectBundle.obj;

       
        newCardinfo.transform.parent = FindEmptyChild();
        newCardinfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        newCardinfo.id = objectBundle.id;

        newCardinfo.GetComponent<RectTransform>().localScale = Vector3.one;
        newCardinfo.GetComponent<RectTransform>().sizeDelta = new Vector2(newCardinfo.transform.parent.GetComponent<RectTransform>().sizeDelta.x,
            newCardinfo.transform.parent.GetComponent<RectTransform>().sizeDelta.y);
        newCardinfo.id = objectBundle.id;


        if (data is CharactorData)
        {
            CharactorData characterData = (CharactorData)data; 
            newCardinfo.icon.sprite = characterData.sprite;
            newCardinfo.cost = characterData.cost;
            newCardinfo.costText.text = characterData.cost.ToString();
            newCardinfo.card = characterData.charater;
        }
        else if(data is EffectData)
        {
            EffectData effectData = (EffectData)data;

            newCardinfo.icon.sprite = effectData.sprite;
            newCardinfo.cost = effectData.cost;
            newCardinfo.costText.text = effectData.cost.ToString();
            newCardinfo.card = effectData.effect;

        }

    }


    public void CompareCardsCost(int cost)
    {

        for(int i = 0; i < cardFixTransform.Count; i++)
        {
            CardInfo cardInfo = cardFixTransform[i].GetChild(0).GetComponent<CardInfo>();
            
            if(cardInfo != null)
            {


               if(cardInfo.cost <= cost)
                {
                    cardInfo.SetButtonPressable(true);
                    
                }
                else 
                {
                    cardInfo.SetButtonPressable(false);
                   
                }

            } 


        }



    }



    public Transform FindEmptyChild()
    {

        for(int i = 0; i < cardFixTransform.Count; i++)
        {

            if(cardFixTransform[i].transform.childCount == 0)
            {
               

                return cardFixTransform[i];

            }


        }

        return null;

    }




}
