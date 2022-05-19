using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentPlayer : Player
{
    public AimObject aimObject;
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

    public float Delaytime = 1f;
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


    public void SetOponentAimObject(AimObject aimObject)
    {

        this.aimObject = aimObject;
    }

    public void MonsterSpwan(Transform transform)
    {
        if (aimObject != null)
        {

            AimObject aimObj = Instantiate(aimObject);           
            aimObj.transform.position = transform.position;
            AddCharacter(aimObj);
            aimObject = null;
        }



    }





    public void Update()
    {
        if(Time.time < curTime + Delaytime)
        {
            return;
        }

        curTime = Time.time;

        OponentCardSelectRoutin();
    }


    
    private void OponentCardSelectRoutin()
    {


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
                cardInfo.aimObject = data.charater;
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
