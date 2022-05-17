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
            cardInfo.id = objectBundle.id;
            cardInfo.icon.sprite = ((CharactorData)data).sprite;
            cardInfo.cost = ((CharactorData)data).cost;
            cardInfo.costText.text = cardInfo.cost.ToString();
            cardInfo.aimObject = ((CharactorData)data).charater;
        }
    }



   public void CardInsert(Player.ObjectBundle objectBundle)
   {

        CardInfo newCardinfo = Instantiate(info);

        if (FindEmptyChild() == null)
        {
            return;
        }

        CharactorData data = (CharactorData)objectBundle.obj;

       
        newCardinfo.transform.parent = FindEmptyChild();
        newCardinfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        newCardinfo.id = objectBundle.id;
        newCardinfo.icon.sprite = data.sprite;
        newCardinfo.cost = data.cost;
        newCardinfo.costText.text = newCardinfo.cost.ToString();
        newCardinfo.aimObject = data.charater;

    }


    public void CompareCardsCost(int cost)
    {

        for(int i = 0; i < cardFixTransform.Count; i++)
        {
            CardInfo cardInfo = cardFixTransform[i].GetChild(0).GetComponent<CardInfo>();
            
            if(cardInfo != null)
            {


               if(cardInfo.cost < cost)
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
