//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MeteoEffect : HandCardEffect
//{

//    public Projectile projectile;


//    public override void CardCilckTrigger(Player player)
//    {
//        StartCoroutine(MeteoRoutin());


//    }


//    private void EventSet(Player player)
//    {

//        var go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);

//        if (go == null)
//        {

//            ObjectPooling.ObjectPoolingManager.Instance
//                .AddObjects(projectile.name, projectile.gameObject, 5);
//            go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);
//        }


//        Projectile pro = go.GetComponent<Projectile>();
//        pro.transform.position = spwanPoint.position;
       

//        if (player.playertype == PlayerType.Oponent)
//        {
//            pro.gameObject.layer = 13;
//        }
//        else if (player.playertype == PlayerType.Own)
//        {
//            pro.gameObject.layer = 12;
//        }



//        pro.effectAction = (Collision collision) =>
//        {
//            AimObject aimObject = collision.gameObject.GetComponent<AimObject>();
//            if (aimObject != null && aimObject.player.playertype != gameObj.player.playertype)
//            {

//                aimObject.Hit(gameObj);

//                //스킬 버프???
//                GameObject particle = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectileParticle.name);
//                particle.transform.position = collision.contacts[0].point;
//                particle.transform.LookAt(collision.contacts[0].normal);

//                pro.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();
//                pro.GetComponent<Rigidbody>().velocity = Vector3.zero;
//            }


//        };


//        pro.groundAction = (Collision collision) => {

//            if (collision.gameObject.tag == "Ground")
//            {

//                GameObject particle = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectileParticle.name);
//                particle.transform.position = collision.contacts[0].point;
//                particle.transform.LookAt(collision.contacts[0].normal);

//                pro.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();
//                pro.GetComponent<Rigidbody>().velocity = Vector3.zero;
//            }

//        };



//    }


//    public Vector3 SetPos()
//    {
//        NodeMapCreate nodeMapCreate = FindObjectOfType<NodeMapCreate>();
//        List<Node> nodes = nodeMapCreate.nodes;
//        int randomIndex = Random.Range(0, nodes.Count);
//        Vector3 pos = new Vector3(nodes[randomIndex].transform.position.x
//               , nodes[randomIndex].transform.position.y + 50f, nodes[randomIndex].transform.position.z);

//        return pos;

//    }


//    IEnumerator MeteoRoutin()
//    {

    
//        WaitForSeconds wait = new WaitForSeconds(0.5f);

//        for (int i = 0; i < 10; i++)
//        {
           

//            yield return wait;
//        }



//    }



//}
