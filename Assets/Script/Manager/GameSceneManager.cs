using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : GameManager<GameSceneManager>
{
    public Player ownPlayer;
    public OponentPlayer oponentPlayer;

    public GameCardBuild gameCardBuild;
    public CostGage costGage;
    public AimObject spwanObjet;
    public CircleRenderer circleRenderer;


    private Coroutine gamecorutin;

    
    public void Awake()
    {
        EventManager.On("GameStart", PlayerInit);
        EventManager.On("GameStart", RandomCardSelet);
        EventManager.On("GameStart", StartGage);
        EventManager.On("GameStart", CardCompare);
        EventManager.On("GameStart", ProjectileEndEffectCreate);

        EventManager.On("CardSelect", CardSelect);
        EventManager.On("CardSelect", RandomCardSelet);
        EventManager.On("CardSelect", CardCompare);

        EventManager.On("GameEnd", StopGage);


        EventManager.On("OponentInit", PlayerInit);
        EventManager.On("OponentInit", RandomCardSelet);
        EventManager.On("OponentInit", AiRoutin);

        EventManager.On("OponentCardSelect", CardSelect);
        EventManager.On("OponentCardSelect", RandomCardSelet);


    }


    public void Start()
    {
        ParameterHelper playerParameter = new ParameterHelper();
        ParameterHelper oponentParameter = new ParameterHelper();

        playerParameter.objList.Add(ownPlayer);
        oponentParameter.objList.Add(oponentPlayer);

        EventManager.Emit("GameStart", playerParameter);
        EventManager.Emit("OponentInit", oponentParameter);
    }


    public void PlayerInit(object parmater)
    {
        ParameterHelper parameterHelper = (ParameterHelper)(parmater);

        Player player = parameterHelper.GetParameter<Player>();
 
        if(player == null)
        {
            Debug.LogError("플레이어 없음");
            return;
        }
  
        player.curCost = player.MaxCost;
        

        int index = 0;
        foreach (CharactorData data in player.characterdatas)
        {
            player.totalCardDatas.Add(index, data);
            index++;
        }

        for (int i = 0; i < 4; i++)
        {
            List<int> indexs = new List<int>();
            for (int j = 0; j < player.totalCardDatas.Count; j++)
            {
                if (player.handCardDatas.Find(x => j == x.id) != null)
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
            objectBundle.obj = player.totalCardDatas[selectindex];
            objectBundle.id = selectindex;

            player.handCardDatas.Add(objectBundle);
        }


        if(player is OponentPlayer)
        {
            return;
        }
        costGage.SetCost(player.curCost);
        gameCardBuild.CardBuildInit();
    }



    // 다음 카드를 랜덤으로 

    public void RandomCardSelet(object parmeter)
    {
        ParameterHelper parameterHelper = (ParameterHelper)parmeter;

        Player player = parameterHelper.GetParameter<Player>();

        if (player == null)
        {
            Debug.LogError("플레이어 없음");
            return;
        }



        List<int> indexs = new List<int>();
        for (int j = 0; j < player.totalCardDatas.Count; j++)
        {
            if (player.handCardDatas.Find(x => j == x.id) != null)
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
        objectBundle.obj = player.totalCardDatas[selectindex];
        objectBundle.id = selectindex;

        //다음 넥스트 카드 
        player.nextCardObj = objectBundle;


        if (player is OponentPlayer) {
            return;
        }
        CharactorData nextData = (CharactorData)(objectBundle.obj);
        gameCardBuild.next.img.sprite = nextData.sprite;
    }



    //다음 카드를 손위로 넣는과정 
    public void CardSelect(object parameter)
    {
        ParameterHelper parameterHelper = (ParameterHelper)(parameter);

        Player player = parameterHelper.GetParameter<Player>();
        CardInfo info = parameterHelper.GetParameter<CardInfo>();


        if (!player.PurchaseCardCost(info.cost))
        {
            return;
        }
       



        Player.ObjectBundle obj = player.handCardDatas.Find(x => x.id == info.id);

        if (player is OponentPlayer)
        {
            OponentPlayer opplayer = (OponentPlayer)(player);
            opplayer.SetOponentAimObject(info.aimObject);
            player.handCardDatas.Remove(obj);
        }
        else
        {
            SetSpwanObject(info.aimObject);
            player.handCardDatas.Remove(obj);
            info.transform.parent = null;
            Destroy(info.gameObject);
        }

      
        Player.ObjectBundle newCard = new Player.ObjectBundle();

        newCard.id = player.nextCardObj.id;
        newCard.obj = player.nextCardObj.obj;
        player.handCardDatas.Add(newCard);

        if(player is OponentPlayer)
        {

            return;
        }



        costGage.SetCost(player.curCost);
        gameCardBuild.CardInsert(newCard);
        

    }

    public void MonsterSpwan(Transform transform)
    {
        if(spwanObjet != null)
        {

           AimObject aimObj  =  Instantiate(spwanObjet);
           aimObj.player = ownPlayer;
           aimObj.transform.position = transform.position;            
           ownPlayer.AddCharacter(aimObj);
           spwanObjet = null;
        }



    }

    public void AiRoutin(object parmeter)
    {
       // oponentPlayer.StartAiRoutin();

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
        WaitForSeconds wait = new WaitForSeconds(0.001f);
        while (true)
        {
            if (ownPlayer.FillGage())
            {
                costGage.SetCost(ownPlayer.curCost);
                
                CardCompare(null);
            }



           
            
            if(oponentPlayer.curFill > oponentPlayer.maxFill)
            {
                oponentPlayer.curFill = oponentPlayer.maxFill;

                if(oponentPlayer.MaxCost >= oponentPlayer.curCost)
                {
                    oponentPlayer.curCost = oponentPlayer.MaxCost;
                    oponentPlayer.curFill = 0;
                }


            }
            oponentPlayer.curFill += oponentPlayer.fillRatio;


            costGage.FillAmountGage(ownPlayer.curFill / ownPlayer.maxFill);
            

            yield return wait;
        }



    }
    
    private void ProjectileEndEffectCreate(object parameter)
    {

        List<CharactorData> ownCharactorDatas =  ownPlayer.characterdatas;
        List<CharactorData> oponentCharactorDatas = oponentPlayer.characterdatas;

        List<CharactorData> totalDatas = new List<CharactorData>();

        totalDatas.AddRange(ownCharactorDatas);
        totalDatas.AddRange(ownCharactorDatas);

        FarAwayAttack awayAttackOwnTower = ownPlayer.tower.GetComponent<FarAwayAttack>();
        FarAwayAttack awayAttackOponentTower = oponentPlayer.tower.GetComponent<FarAwayAttack>();

        if(awayAttackOwnTower != null)
        {

            if (awayAttackOwnTower.projectile != null)
            {
                ObjectPooling.ObjectPoolingManager.Instance.
                       AddObjects(awayAttackOwnTower.projectile.name, awayAttackOwnTower.projectile.gameObject, 5);
            }

            if (awayAttackOwnTower.projectileParticle != null)
            {
                ObjectPooling.ObjectPoolingManager.Instance.
                        AddObjects(awayAttackOwnTower.projectileParticle.name, awayAttackOwnTower.projectileParticle.gameObject, 5);
            }

        }

        if(awayAttackOponentTower != null)
        {
            if (awayAttackOponentTower.projectile != null)
            {
                ObjectPooling.ObjectPoolingManager.Instance.
                       AddObjects(awayAttackOponentTower.projectile.name, awayAttackOponentTower.projectile.gameObject, 5);
            }
            if (awayAttackOponentTower.projectileParticle != null)
            {
                ObjectPooling.ObjectPoolingManager.Instance.
                        AddObjects(awayAttackOponentTower.projectileParticle.name, awayAttackOponentTower.projectileParticle.gameObject, 5);
            }
        }



        foreach(var data in totalDatas)
        {
            FarAwayAttack farAwayAttack = data.charater.GetComponent<FarAwayAttack>();
            if (farAwayAttack != null)
            {



                if (farAwayAttack.projectile != null)
                {
                    ObjectPooling.ObjectPoolingManager.Instance.
                        AddObjects(farAwayAttack.projectile.name, farAwayAttack.projectile.gameObject, 5);
                }

                if (farAwayAttack.projectileParticle != null)
                {
                    ObjectPooling.ObjectPoolingManager.Instance.
                        AddObjects(farAwayAttack.projectileParticle.name, farAwayAttack.projectileParticle.gameObject, 5);
                }



            }

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
