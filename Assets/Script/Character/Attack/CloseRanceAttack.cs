using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRanceAttack : Attack , INodeTileDetect
{
   

    override public void init(AimObject charater)
    {

        gameObj = charater;
        detect = new GroundDetect(gameObj);

    }
    override public void AttackTarget(AimObject target)
    {
        if(target == null)
        {
            return;
        }

        float damage = gameObj.GetComponent<IObjectInfo>().GetDamage();
        target.Hit(gameObj);

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


            if (Physics.Raycast(aimObject.transform.position, Vector3.down, out hit, LayerMask.GetMask("Node")))
            {
                Node n = hit.collider.GetComponent<Node>();



                if (n == null)
                {
                    continue;
                }


                if (n.nodetype == NodeType.Oponent)
                {

                    nodes.Add(n);
                }
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

            if (t.value == temporary.value)
            {


                finds.Add(t);
            }
            else
            {
                break;
            }
        }


        if(finds.Count == 0)
        {

            return null;
        }

        int random = Random.Range(0, finds.Count);

        


        Node nodefind = (Node)finds[random].obj;

        return nodefind;

    }
}
