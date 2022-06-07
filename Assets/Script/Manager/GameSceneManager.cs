using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class GameSceneManager : GameManager<GameSceneManager>
{
    public Player ownPlayer;
    public OponentPlayer oponentPlayer;
    public GameCardBuild gameCardBuild;
    public OponentInfoGroup opoenentinfoGroup;
    public CostGage costGage;
    public Card spwanObjet;
    public CircleRenderer circleRenderer;
    public GameStateUi stateUi;

    private Coroutine gamecorutin;

    
    public void Awake()
    {
        EventManager.On("GamePlayinit", GamePlayerLoadData);
        EventManager.On("GamePlayinit", GameOponentDataLoad);
        EventManager.On("GamePlayinit", GameReady);

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
        EventManager.Emit("GamePlayinit", null);
    }


    public void GamePlayerLoadData(object parameter)
    {
        ownPlayer.cardDatas.Clear();
        string pathData = File.ReadAllText(Application.dataPath + "/player.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(pathData);

        List<string> deckCardnames  =  playerData.cardDackNames;
        
        for(int i = 0; i < deckCardnames.Count; i++)
        {
            string cardDatapath = string.Format("data/{0}", deckCardnames[i]);
            ScriptableObject data = Resources.Load<CharactorData>(cardDatapath);

            if (data == null)
            {
                string effectDatapath = string.Format("EffectData/{0}", deckCardnames[i]);
                data = Resources.Load<EffectData>(effectDatapath);

                if(data == null)
                {
                    Debug.LogError("카드 데이터 x");
                    return;
                }

            }
            
            ownPlayer.cardDatas.Add(data);
            
        }

    



        string towername = playerData.towerName;
        string towerDatapath = string.Format("TowerData/{0}", towername);
        TowerData towerData = Resources.Load<TowerData>(towerDatapath);

        if(towerData == null)
        {
            Debug.LogError("타워 데이터 x");
            return;
        }

        ownPlayer.tower = (Tower)towerData.aimObject;
        ownPlayer.TowerSet();
    }


    public void GameOponentDataLoad(object parameter)
    {
        oponentPlayer.cardDatas.Clear();
       
        RerayDataManager rerayDataManager = RerayDataManager.Instance;

        for(int i = 0; i < rerayDataManager.charactorDatas.Count; i++)
        {
            oponentPlayer.cardDatas.Add(rerayDataManager.charactorDatas[i]);
        }

        for(int i = 0; i < rerayDataManager.effectDatas.Count; i++)
        {
            oponentPlayer.cardDatas.Add(rerayDataManager.effectDatas[i]);

        }

        oponentPlayer.tower =  (Tower)rerayDataManager.towerData.aimObject;
        oponentPlayer.TowerSet();

    }

    public void GameReady(object parameter)
    {
        ownPlayer.gameStart = false;
        oponentPlayer.gameStart = false;

        stateUi.GameStart();

        //Invoke("GameStart", 2f);
    }

    public void GameStart()
    {
       
        ownPlayer.gameStart = true;
        oponentPlayer.gameStart = true;

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
        foreach (ScriptableObject data in player.cardDatas)
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


        int random = Random.Range(0, indexs.Count);

        int selectindex = indexs[random];

        Player.ObjectBundle objectBundle = new Player.ObjectBundle();
        objectBundle.obj = player.totalCardDatas[selectindex];
        objectBundle.id = selectindex;

        //다음 넥스트 카드 
        player.nextCardObj = objectBundle;


        if (player is OponentPlayer) {
            return;
        }

        if(objectBundle.obj is CharactorData)
        {
            CharactorData nextData = (CharactorData)(objectBundle.obj);
            gameCardBuild.next.img.sprite = nextData.sprite;

        }
        else if(objectBundle.obj is EffectData)
        {
            EffectData nextData = (EffectData)(objectBundle.obj);
            gameCardBuild.next.img.sprite = nextData.sprite;

        }

       
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
            opplayer.SetOponentAimObject(info.card);
            player.handCardDatas.Remove(obj);
        }
        else
        {
            SetSpwanObject(info.card);
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

           AimObject aimObj  =  (AimObject)Instantiate(spwanObjet);
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

        List<ScriptableObject> ownCharactorDatas =  ownPlayer.cardDatas;
        List<ScriptableObject> oponentCharactorDatas = oponentPlayer.cardDatas;

        List<ScriptableObject> totalDatas = new List<ScriptableObject>();

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
            if(data is EffectData)
            {
                continue;
            }


            CharactorData charactorData = (CharactorData)data;

            FarAwayAttack farAwayAttack = charactorData.charater.GetComponent<FarAwayAttack>();
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


    public void SceneTransition()
    {
        SceneManager.LoadScene("StageScene");


    }

    public void SetSpwanObject(Card obj)
    {
      
        if(obj.GetComponent<ICardClickTrigger>() != null)
        {
            obj.GetComponent<ICardClickTrigger>().CardCilckTrigger(ownPlayer);
            return;
        }

        spwanObjet = obj;
    }

    

 

}
