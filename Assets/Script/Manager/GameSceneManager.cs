using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : GameManager<GameSceneManager>
{
    public Player ownPlayer;
    public Player oponentPlayer;

    public GameCardBuild gameCardBuild;
    public CostGage costGage;
    public AimObject spwanObjet;

    private Coroutine gamecorutin;


    public void Awake()
    {
        EventManager.On("GameStart", PlayerInit);
        EventManager.On("GameStart", RandomCardSelet);
        EventManager.On("GameStart", StartGage);
        EventManager.On("GameStart", CardCompare);
        EventManager.On("CardSelect", CardSelect);
        EventManager.On("CardSelect", RandomCardSelet);
        EventManager.On("CardSelect", CardCompare);
        EventManager.On("GameEnd", StopGage);
    }


    public void Start()
    {
        EventManager.Emit("GameStart", null);
    }


    public void GameSceneInit(Player own , Player oponent)
    {
        ownPlayer = own;
        oponentPlayer = oponent;

        ownPlayer.playertype = PlayerType.Own;
        oponentPlayer.playertype = PlayerType.Oponent;
    }




    public void PlayerInit(object parmater)
    {
        ownPlayer.curCost = ownPlayer.MaxCost;

        costGage.SetCost(ownPlayer.curCost);


        int index = 0;
        foreach (CharactorData data in ownPlayer.characterdatas)
        {
            ownPlayer.totalCardDatas.Add(index, data);
            index++;
        }

        for (int i = 0; i < 4; i++)
        {
            List<int> indexs = new List<int>();
            for (int j = 0; j < ownPlayer.totalCardDatas.Count; j++)
            {
                if (ownPlayer.handCardDatas.Find(x => j == x.id) != null)
                {
                    continue;
                }

                indexs.Add(j);
            }


            if (indexs.Count == 0)
            {
                return;
            }


            int random = Random.Range(0, indexs.Count - 1);

            int selectindex = indexs[random];

            Player.ObjectBundle objectBundle = new Player.ObjectBundle();
            objectBundle.obj = ownPlayer.totalCardDatas[selectindex];
            objectBundle.id = selectindex;

            ownPlayer.handCardDatas.Add(objectBundle);
        }

        gameCardBuild.CardBuildInit();
    }

    public void RandomCardSelet(object parmeter)
    {
        List<int> indexs = new List<int>();
        for (int j = 0; j < ownPlayer.totalCardDatas.Count; j++)
        {
            if (ownPlayer.handCardDatas.Find(x => j == x.id) != null)
            {
                continue;
            }

            indexs.Add(j);
        }


        if (indexs.Count == 0)
        {
            return;
        }


        int random = Random.Range(0, indexs.Count - 1);

        int selectindex = indexs[random];

        Player.ObjectBundle objectBundle = new Player.ObjectBundle();
        objectBundle.obj = ownPlayer.totalCardDatas[selectindex];
        objectBundle.id = selectindex;

        //다음 넥스트 카드 
        ownPlayer.nextCardObj = objectBundle;
        CharactorData nextData = (CharactorData)(objectBundle.obj);
        gameCardBuild.next.img.sprite = nextData.sprite;
    }

    public void CardSelect(object parameter)
    {

       
        CardInfo info = (CardInfo)parameter;

        if (!ownPlayer.PurchaseCardCost(info.cost))
        {
            return;
        }
        costGage.SetCost(ownPlayer.curCost);



        Player.ObjectBundle obj = ownPlayer.handCardDatas.Find(x => x.id == info.id);
        SetSpwanObject(info.aimObject);
        ownPlayer.handCardDatas.Remove(obj);
        info.transform.parent = null;
        Destroy(info.gameObject);


        Player.ObjectBundle newCard = new Player.ObjectBundle();

        newCard.id = ownPlayer.nextCardObj.id;
        newCard.obj = ownPlayer.nextCardObj.obj;
        ownPlayer.handCardDatas.Add(newCard);
        gameCardBuild.CardInsert(newCard);
        

    }

    public void CardCompare(object parameter)
    {

        gameCardBuild.CompareCardsCost(ownPlayer.curCost);
    }
    
    void StartGage(object parameter)
    {


        StartCoroutine(GamePlayerGageRoutin());

    }

    
    void StopGage(object parameter)
    {

        StopCoroutine(gamecorutin);
        
    }

    


    IEnumerator GamePlayerGageRoutin()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        while (true)
        {
            if (ownPlayer.FillGage())
            {
                costGage.SetCost(ownPlayer.curCost);
                
                CardCompare(null);
            }
            oponentPlayer.FillGage();

            costGage.FillAmountGage(ownPlayer.curFill / ownPlayer.maxFill);
            

            yield return wait;
        }



    }
    

    public void SetSpwanObject(AimObject obj)
    {
        spwanObjet = obj;
    }



    private void OnMouseDown()
    {
        


    }

}
