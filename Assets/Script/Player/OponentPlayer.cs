using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentPlayer : Player , Iinformation
{
    public Card card;
    public Coroutine oponentAIRoutin;
    public OponentAIState aiState = OponentAIState.Select;

    private NodeMapCreate nodeMap;



    public Node[,] nodes;
    
    [HideInInspector]
    public int maxX;
    [HideInInspector]
    public int maxY;

    public NodeDetect detectOponentArea ;
    public NodeDetect detectRandom;

    public float Delaytime = 2f;
    public float curTime = 0f;


    private Node findNode;
    public void Awake()
    {

        fillRatio = 0.001f;
        nodeMap = FindObjectOfType<NodeMapCreate>();


        maxX = nodeMap.mapSizeX;
        maxY = nodeMap.mapSizeY;

        nodes = new Node[maxX, maxY];


        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                nodes[x, y] = nodeMap.nodes[y * maxX + x];
                nodes[x, y].nodeX = x;
                nodes[x, y].nodeY = y;

               
               
            }
        }


        detectOponentArea = new OponentAreaDetect(this);
        detectRandom = new RandomNodeDetect(this);

  

       
    }


    public void SetOponentAimObject(Card card)
    {
        if(card.GetComponent<ICardClickTrigger>() != null)
        {
            card.GetComponent<ICardClickTrigger>().CardCilckTrigger(this);

            if(card.data is EffectData)
            {
                EffectData data = (EffectData)card.data;

                string comment = string.Format("{0} 사용 !", data.effectName);

                informationPrint(comment);
            }


            return;
        }

        this.card = card;
    }

    public void MonsterSpwan(Transform transform)
    {
        if (card != null)
        {

            if (card.GetComponent<ITileCilckTrigger>() != null)
            {

                card.GetComponent<ITileCilckTrigger>().TileCilckTrigger(findNode, this);
                findNode.DestroyCircle(this);

            }
            else
            {


                AimObject aimObj = (AimObject)Instantiate(card);
                aimObj.transform.position = transform.position;
                AddCharacter(aimObj);
            }
            card = null;
        }



    }





    public void Update()
    {
        if(Time.time < curTime + Delaytime)
        {
            return;
        }

        curTime = Time.time;

        // OponentCardSelectRoutin();

        AiCardSelection();
    }



    private void AiCardSelection()
    {
        if (!gameStart)
        {
            return;
        }

        if(aiState == OponentAIState.Select)
        {
            List<ObjectBundle> conditionObjs = new List<ObjectBundle>();
            List<ObjectBundle> spwanableObjs = new List<ObjectBundle>();

            foreach (ObjectBundle handCardData in handCardDatas)
            {
               
               

                if (handCardData.obj is CharactorData)
                {
                    CharactorData charactorData = (CharactorData)handCardData.obj;
                    if (charactorData.cost <= curCost)
                    {

                        spwanableObjs.Add(handCardData);
                    }

                }
                else if (handCardData.obj is EffectData )
                {
                    EffectData effectData = (EffectData)handCardData.obj;

                    if (effectData.cost <= curCost)
                    {
                        spwanableObjs.Add(handCardData);
                    }
                }

            }

            if (spwanableObjs.Count == 0)
            {
                return;
            }



            foreach (ObjectBundle spwanableObj in spwanableObjs)
            {
                CharactorData charactorData = null;
                EffectData effectData = null;

                if(spwanableObj.obj is CharactorData)
                {
                    charactorData = (CharactorData)spwanableObj.obj;
                }

                if (spwanableObj.obj is EffectData)
                {
                    effectData = (EffectData)spwanableObj.obj;
                }

                if (charactorData != null && charactorData.charater.GetComponent<ICardSelectCondition>() != null  
                    && charactorData.charater.GetComponent<ICardSelectCondition>().CardSelection())
                {
                    if (curCost >= charactorData.cost)
                    {

                        conditionObjs.Add(spwanableObj);
                    }
                }

                if (effectData != null && effectData.effect.GetComponent<ICardSelectCondition>() != null
                    && effectData.effect.GetComponent<ICardSelectCondition>().CardSelection())
                {
                    if (curCost >= effectData.cost)
                    {

                        conditionObjs.Add(spwanableObj);
                    }
                }




            }

           if(conditionObjs.Count == 0)
            {
                int random = Random.Range(0, spwanableObjs.Count);



                ObjectBundle selectObj = spwanableObjs[random];
                CharactorData characterdata =null;
                EffectData effectData = null;

                if(selectObj.obj is CharactorData)
                {

                    characterdata = (CharactorData)selectObj.obj;
                }
                if (selectObj.obj is EffectData)
                {
                    effectData = (EffectData)selectObj.obj;

                }



                CardInfo cardInfo = new CardInfo();
                if (characterdata != null)
                {
                    
                    cardInfo.id = selectObj.id;
                    cardInfo.card = characterdata.charater;
                    cardInfo.cost = characterdata.cost;


                }
                else if(effectData != null)
                {
                    cardInfo.id = selectObj.id;
                    cardInfo.card = effectData.effect;
                    cardInfo.cost = effectData.cost;
                }               
                ParameterHelper parameterHelper = new ParameterHelper();
                parameterHelper.objList.Add(cardInfo);
                parameterHelper.objList.Add(this);

                EventManager.Emit("OponentCardSelect", parameterHelper);

                aiState = OponentAIState.TileDetect;

                return;
            }

            // 1. 필드위에 상대 몬스터 중에 fly 몬스터가 있나 -> 원거리 몬스터 선택 or fly몬스터 선택 or effect 
            // 2. 필드위에 상대 몬스터가 ground 모두일 경우 모두 만족 가장 큰 cost 카드  

           Player own =  GameSceneManager.Instance.ownPlayer;
           List<AimObject> charaters =  own.GetObjects<AimObject>();
           var flyObj  =  charaters.Find(x => x.type == GameObjType.Fly);

            List<ObjectBundle> selets = new List<ObjectBundle>();
            
           if(flyObj != null)
           {
                foreach(ObjectBundle condition in conditionObjs)
                {
                    CharactorData charactorData = (CharactorData)condition.obj;
                   

                    if(charactorData != null && charactorData.charater.type == GameObjType.Ground)
                    {
                        continue;
                    }

                    selets.Add(condition);
                }
           }
           else
           {
                selets.AddRange(conditionObjs);
           }


           TemporaryData temporaryData = new TemporaryData();
            temporaryData.obj = null;
            temporaryData.value = 0;

           foreach(ObjectBundle select in selets)
            {
                CharactorData characterdata = null;
                EffectData effectData = null;

                if (select.obj is CharactorData)
                {

                    characterdata = (CharactorData)select.obj;
                }
                if (select.obj is EffectData)
                {
                    effectData = (EffectData)select.obj;

                }

                if (characterdata != null && characterdata.cost >= (int)temporaryData.value)
                {
                    temporaryData.obj = select;
                    temporaryData.value = characterdata.cost;

                }else if(effectData != null && effectData.cost >= (int)temporaryData.value)
                {
                    temporaryData.obj = select;
                    temporaryData.value = effectData.cost;


                }
            }


            if (temporaryData.obj != null)
            {

                ObjectBundle selectObj = (ObjectBundle)temporaryData.obj;
                CharactorData characterdata = null;
                EffectData effectData = null;

                if (selectObj.obj is CharactorData)
                {

                    characterdata = (CharactorData)selectObj.obj;
                }
                if (selectObj.obj is EffectData)
                {
                    effectData = (EffectData)selectObj.obj;

                }
                CardInfo cardInfo = new CardInfo();
                if (characterdata != null)
                {

                    cardInfo.id = selectObj.id;
                    cardInfo.card = characterdata.charater;
                    cardInfo.cost = characterdata.cost;


                }
                else if (effectData != null)
                {
                    cardInfo.id = selectObj.id;
                    cardInfo.card = effectData.effect;
                    cardInfo.cost = effectData.cost;
                }

                ParameterHelper parameterHelper = new ParameterHelper();
                parameterHelper.objList.Add(cardInfo);
                parameterHelper.objList.Add(this);

                EventManager.Emit("OponentCardSelect", parameterHelper);

                aiState = OponentAIState.TileDetect;

            }

        }
        else if(aiState == OponentAIState.TileDetect)
        {
          
            if(card == null)
            {
                aiState = OponentAIState.Select;
                return;
            }

            if (card.GetComponent<ICardClickTrigger>() != null)
            {
                aiState = OponentAIState.Select;
                return;
            }

            findNode = card.GetComponent<INodeTileDetect>().NodeDetect(this);

          


            if(card.GetComponent<ITileCilckTrigger>() != null)
            {
                aiState = OponentAIState.SpwanMonster;

                //카드 사용 

                if (findNode != null)
                {

                    findNode.EffectCircleCreate(this);
                }
                else
                {
                    
                    findNode = detectRandom.DetectTiles();
                    findNode.EffectCircleCreate(this);
                    aiState = OponentAIState.SpwanMonster;
                }



              
            }
            else
            {
                if (findNode != null)
                {
                    findNode.renderer.enabled = true;

                    aiState = OponentAIState.SpwanMonster;

                }
                else
                {
                    findNode = detectRandom.DetectTiles();
                    findNode.renderer.enabled = true;
                    aiState = OponentAIState.SpwanMonster;

                }
       

            }

            string comment = "";

            if(card.data is CharactorData)
            {
                CharactorData charactorData = (CharactorData)card.data;
                comment = string.Format("{0} 소환!!", charactorData.characterName);

            }
            else if(card.data is EffectData)
            {
                EffectData EffectData = (EffectData)card.data;
                comment = string.Format("{0} 사용!!", EffectData.effectName);
            }

            informationPrint(comment);


        }
        else
        {
            findNode.renderer.enabled = false;
            MonsterSpwan(findNode.transform);
            aiState = OponentAIState.Select;
        }

    }

     public void informationPrint(string str)
     {
        GameSceneManager.Instance.opoenentinfoGroup.CreateInfoUi(str);
     }

    private void OponentCardSelectRoutin()
    {
        if (!gameStart)
        {
            return;
        }

        if (aiState == OponentAIState.Select)
        {

            List<ObjectBundle> spwanableObjs = new List<ObjectBundle>();

            foreach (ObjectBundle handCardData in handCardDatas)
            {
                CharactorData charactorData = (CharactorData)handCardData.obj;

                if (curCost >= charactorData.cost)
                {

                    spwanableObjs.Add(handCardData);
                }
            }


            if (spwanableObjs.Count == 0)
            {
                return;
            }


            Player player = GameSceneManager.Instance.ownPlayer;
            List<AimObject> charaters = player.GetObjects<AimObject>();
            TemporaryData temporaryData = new TemporaryData();
            if (charaters.Count == 0)
            {
                int minCost = this.MaxCost;
                ObjectBundle objectBundle = null;

                foreach (ObjectBundle handCardData in handCardDatas)
                {
                    CharactorData data = (CharactorData)(handCardData.obj);

                    if (minCost > data.cost)
                    {
                        objectBundle = handCardData;
                        minCost = data.cost;
                    }


                }

                if (objectBundle != null)
                {
                    temporaryData.obj = objectBundle;
                }

            }
            else
            {
                AimObject maxAttackCharacter = new AimObject();

                for (int i = 0; i < charaters.Count; i++)
                {

                    if (i == 0)
                    {
                        maxAttackCharacter = charaters[i];
                        continue;
                    }

                    if (maxAttackCharacter.damage < charaters[i].damage)
                    {

                        maxAttackCharacter = charaters[i];

                    }

                }


                for (int i = 0; i < spwanableObjs.Count; i++)
                {
                    CharactorData temporaryCharactor = (CharactorData)spwanableObjs[i].obj;
                    if (i == 0)
                    {
                        temporaryData.obj = spwanableObjs[i];

                        temporaryData.value = Mathf.Abs(maxAttackCharacter.damage - temporaryCharactor.damage);
                        continue;

                    }


                    float value = Mathf.Abs(maxAttackCharacter.damage - temporaryCharactor.damage);
                    if (maxAttackCharacter.damage < value)
                    {

                        temporaryData.obj = spwanableObjs[i];
                        temporaryData.value = value;

                    }


                }

            }


            if (temporaryData.obj != null)
            {

                ObjectBundle selectObj = (ObjectBundle)temporaryData.obj;
                CharactorData data = (CharactorData)selectObj.obj;

                CardInfo cardInfo = new CardInfo();
                cardInfo.id = selectObj.id;
                cardInfo.card = data.charater;
                cardInfo.cost = data.cost;


                ParameterHelper parameterHelper = new ParameterHelper();
                parameterHelper.objList.Add(cardInfo);
                parameterHelper.objList.Add(this);

                EventManager.Emit("OponentCardSelect", parameterHelper);

                aiState = OponentAIState.TileDetect;

            }

        }
        else if (aiState == OponentAIState.TileDetect)
        {

            findNode = detectOponentArea.DetectTiles();

            if (findNode != null)
            {
                findNode.renderer.enabled = true;

                aiState = OponentAIState.SpwanMonster;

            }
            else
            {
                findNode = detectRandom.DetectTiles();
                findNode.renderer.enabled = true;
                aiState = OponentAIState.SpwanMonster;
            }

        }
        else
        {
            findNode.renderer.enabled = false;
            MonsterSpwan(findNode.transform);
            aiState = OponentAIState.Select;
        }




    }

   
  
}


public enum OponentAIState
{
    Select,

    TileDetect,

    SpwanMonster,



}
