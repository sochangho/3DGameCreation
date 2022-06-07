using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentTargetEffect : Effect , ITileCilckTrigger , INodeTileDetect
{



    public int range;


    public void TileCilckTrigger(Node node, Player player)
    {
       Collider[]  colliders = Physics.OverlapSphere(node.transform.position, range);

        // 이 노드 위치에 파티클 생성;

       
       for(int i = 0; i < colliders.Length; i++)
       {
           
            IAttacked attacked = colliders[i].GetComponent<IAttacked>();
            
            if(attacked == null)
            {
                continue;
            }

            if(attacked.AttackedObjectType() == player.playertype)
            {
                continue;
            }

            

            AimObject aimObject = colliders[i].GetComponent<AimObject>(); 
            
            if(aimObject == null)
            {
                continue;
            }

            for(int j = 0; j< buffs.Count; j++)
            {
              

                aimObject.buffController.AddBuff(buffs[j].GetCloneBuff() , buffs[j].buffEffect);

            }

       }



    }

    public float GetEffectRenge()
    {
        return range;
    }

    public override bool CardSelection()
    {

        var objects = GameSceneManager.Instance.ownPlayer.GetObjects<AimObject>();

        if (objects.Count > 1)
        {

            return true;
        }


        return false;
    }

    public Node NodeDetect(OponentPlayer player)
    {

        GameSceneManager gsm = GameSceneManager.Instance;
        List<AimObject> aimObjects = new List<AimObject>();
        aimObjects.AddRange(gsm.ownPlayer.GetObjects<AimObject>());



        List<Node> nodes = new List<Node>();


        for (int i = 0; i < aimObjects.Count; i++)
        {
            AimObject aimObject = aimObjects[i];
            RaycastHit hit;


            if (aimObject is Tower)
            {
                continue;
            }

            if (Physics.Raycast(aimObject.transform.position, Vector3.down, out hit, LayerMask.GetMask("Node")))
            {
                Node n = hit.collider.GetComponent<Node>();



                if (n == null)
                {
                    continue;
                }

                if (n.nodetype == NodeType.None)
                {
                    continue;
                }

                nodes.Add(n);
                
            }
        }

        if (nodes.Count == 0)
        {
            return null;
        }



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

        Heap heap = new MaxHeap();


        for (int y = 0; y < player.maxY; y++)
        {
            for (int x = 0; x < player.maxX; x++)
            {
                
                    TemporaryData temporaryData = new TemporaryData();
                    temporaryData.obj = player.nodes[x, y];
                    temporaryData.value = nodeWeight[x, y].weigt;
                    heap.Add(temporaryData);
                

            }
        }

        TemporaryData temporary = heap.RemoveOne();
        List<TemporaryData> finds = new List<TemporaryData>();
        while (true)
        {
            TemporaryData t = heap.RemoveOne();

            if (t.value == temporary.value)
            {


                finds.Add(t);
            }
            else
            {
                break;
            }
        }

        if (finds.Count == 0)
        {

            return null;
        }



        int random = Random.Range(0, finds.Count);

        Node nodefind = (Node)finds[random].obj;

        return nodefind;

    }
}

