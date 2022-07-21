using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FarAwayAttack : Attack 
{
    public Projectile projectile;
    public ProjectileParticle projectileParticle;



    public Transform spwanPoint;
    public Action projectileAtion;

    public float speed;

    public float Speed
    {

        get
        { float v = speed;

            //버프 설정????

            return v;
        }
    }


    



    override public void init(AimObject charater)
    {
        gameObj = charater;
        detect = new AllDectect(gameObj);
    }

    override public void AttackTarget(AimObject target)
    {
        //TODO : 효과추가 , 스킬 , 공격등 , ObjectPooling 이용하기

        //오브젝트 풀로 바꾼다.
        var go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);

        if(go == null)
        {

            ObjectPooling.ObjectPoolingManager.Instance
                .AddObjects(projectile.name, projectile.gameObject, 5);
            go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);
        }


        Projectile pro = go.GetComponent<Projectile>();
        pro.transform.position = spwanPoint.position;
        ProjectileWay(pro);
        pro.projectileWayAction = projectileAtion;
        
        if(gameObj.player.playertype == PlayerType.Oponent)
        {
            pro.gameObject.layer = 13;
        }else if(gameObj.player.playertype == PlayerType.Own)
        {
            pro.gameObject.layer = 12;
        }


      
            pro.effectAction = (Collision collision) =>
            {
                AimObject aimObject = collision.gameObject.GetComponent<AimObject>();
                if (aimObject != null && aimObject.player.playertype != gameObj.player.playertype)
                {

                    aimObject.Hit(gameObj);

                    //스킬 버프???
                    GameObject particle = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectileParticle.name);
                    particle.transform.position = collision.contacts[0].point;
                    particle.transform.LookAt(collision.contacts[0].normal);

                    pro.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();
                    pro.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

         
            };


        pro.groundAction = (Collision collision) => {

            if (collision.gameObject.tag == "Ground")
            {
               
                GameObject particle = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectileParticle.name);
                particle.transform.position = collision.contacts[0].point;
                particle.transform.LookAt(collision.contacts[0].normal);

                pro.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();
                pro.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

        };

        
        pro.projectileAttack();

    }

    virtual public void ProjectileWay(Projectile projectile)
    {
       

    }



   override public Node NodeDetect(OponentPlayer player)
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

        Heap heap = new MinHeap();


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
        if (finds.Count == 0)
        {

            return null;
        }



        int random = UnityEngine.Random.Range(0, finds.Count);

        Node nodefind = (Node)finds[random].obj;

       
        return nodefind;

    }



}

