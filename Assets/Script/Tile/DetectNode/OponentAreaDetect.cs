using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentAreaDetect : NodeDetect
{
    
    public OponentAreaDetect(OponentPlayer player) :base(player)
    {}


    public override Node DetectTiles()
    {
       
       
        GameSceneManager gsm = GameSceneManager.Instance;
        List<AimObject> aimObjects = new List<AimObject>();
        aimObjects.AddRange(gsm.ownPlayer.GetObjects<AimObject>());

       

        List<Node> detectNode = new List<Node>();


        for(int i = 0; i < aimObjects.Count; i++)
        {
            AimObject aimObject = aimObjects[i];
            RaycastHit hit;

            
            if (Physics.Raycast(aimObject.transform.position , Vector3.down ,out hit , LayerMask.GetMask("Node")))
            {
                Node n = hit.collider.GetComponent<Node>();

               

                if(n == null)
                {
                    continue;
                }

              
                if (n.nodetype == NodeType.Oponent)
                {
                    
                    detectNode.Add(n);
                }
            }
        }

        if (detectNode.Count == 0)
        {
            return null;
        }

        //적 진영에 올라와 있는 타일들 
        //----------------------------------------------------------------------------------------------------------------------------------------------------


        Node findNode = null;

        if (player.aimObject.GetComponent<CloseRanceAttack>() != null)
        {
            List<TemporaryData> datas = MonsterweightDetect(detectNode , new MaxHeap());

            TemporaryData temporaryData = new TemporaryData();

            for (int i = 0; i < datas.Count; i++)
            {
                Node node = (Node)datas[i].obj;
                float distacne = Vector3.Distance(node.transform.position, player.tower.transform.position);
                if (i == 0)
                {
                    temporaryData.obj = node;
                    temporaryData.value = distacne;
                    continue;
                }

                if (temporaryData.value > distacne)
                {

                    temporaryData.obj = node;
                    temporaryData.value = distacne;
                }

            }



            findNode = (Node)temporaryData.obj;
        }
        else if(player.aimObject.GetComponent<FarAwayAttack>() != null)
        {

            List<TemporaryData> datas = MonsterweightDetect(detectNode , new MinHeap());


            TemporaryData temporaryData = new TemporaryData();

            for (int i = 0; i < detectNode.Count; i++)
            {
                
                float distacne = Vector3.Distance(detectNode[i].transform.position, player.tower.transform.position);
                if (i == 0)
                {
                    temporaryData.obj = detectNode[i];
                    temporaryData.value = distacne;
                    continue;
                }

                if (temporaryData.value > distacne)
                {

                    temporaryData.obj = detectNode[i];
                    temporaryData.value = distacne;
                }

            }


            TemporaryData monsterOnNodeData = temporaryData;
            Node monsterOnNode = (Node)monsterOnNodeData.obj;



            for (int i = 0; i < datas.Count; i++)
            {
                Node node = (Node)datas[i].obj;
                float distacne = Vector3.Distance(node.transform.position, monsterOnNode.transform.position);
                float rangeCompareDistace = Mathf.Abs(distacne - player.aimObject.range);

                if (i == 0)
                {
                    temporaryData.obj = node;
                    temporaryData.value = rangeCompareDistace;
                    continue;
                }

                if (temporaryData.value > distacne)
                {

                    temporaryData.obj = node;
                    temporaryData.value = rangeCompareDistace;
                }

            }
            findNode = (Node)temporaryData.obj;



        }

      

        return findNode;

    }


    List<TemporaryData> MonsterweightDetect(List<Node> nodes , Heap h) 
    {
        
        
        NodeWeight[,] nodeWeight = new NodeWeight[player.maxX, player.maxY];

        for (int y = 0; y < player.maxY; y++)
        {
            for (int x = 0; x < player.maxX; x++)
            {
                nodeWeight[x, y] = new NodeWeight();
                
                nodeWeight[x, y].weigt = 0;
            }
        }


        

        foreach (Node node in nodes)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int x = node.nodeX;
                    int y = node.nodeY;

                    x += i;
                    y += j;


                    if (x >= 0 && x < player.maxX && y >= 0 && y < player.maxY && !(i == 0 && j == 0)
                    && !player.nodes[x, y].MonsterExist())
                    {

                        nodeWeight[x, y].weigt += 1;
                    }
                }
            }

        }

        Heap heap = h;


        for (int y = 0; y < player.maxY; y++)
        {
            for (int x = 0; x < player.maxX; x++)
            {
                if (player.nodes[x, y].nodetype == NodeType.Oponent)
                {
                    TemporaryData temporaryData = new TemporaryData();
                    temporaryData.obj = player.nodes[x, y];
                    temporaryData.value = nodeWeight[x, y].weigt;
                    heap.Add(temporaryData);
                }
                
            }
        }

        TemporaryData temporary = heap.RemoveOne();
        List<TemporaryData> finds = new List<TemporaryData>();
        while (true)
        {
            TemporaryData t = heap.RemoveOne();

            if (t.value == temporary.value) {

              
                finds.Add(t);
            }
            else
            {
                break;
            }
        }

        finds.Add(temporary);


        return finds;
    }

}
