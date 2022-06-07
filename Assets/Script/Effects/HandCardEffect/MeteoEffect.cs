using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoEffect : HandCardEffect
{

    public Projectile projectile;
    public ProjectileParticle projectileParticle;

    

    
    private NodeMapCreate mapCreate;




    public float value;

    public override void CardCilckTrigger(Player player)
    {
        mapCreate = FindObjectOfType<NodeMapCreate>();
        List<Node> nodes = mapCreate.nodes;

        for (int i = 0; i < 20; i++)
        {
            int randomIndex = Random.Range(0, nodes.Count);

            Vector3 pos = new Vector3(nodes[randomIndex].transform.position.x
                , nodes[randomIndex].transform.position.y + 100f,
                nodes[randomIndex].transform.position.z);

            ProjectileInit(player, pos);


        }


    }

    private void ProjectileInit(Player player , Vector3 pos)
    {
        var go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);

        if (go == null)
        {

            ObjectPooling.ObjectPoolingManager.Instance
                .AddObjects(projectile.name, projectile.gameObject, 5);
            go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);
        }

        Projectile pro = go.GetComponent<Projectile>();
        pro.transform.position = pos;
        pro.projectileWayAction = () =>
        {

            pro.GetComponent<Rigidbody>().AddForce(Vector3.down * 30, ForceMode.Force);


        };


        if (player.playertype == PlayerType.Oponent)
        {
            pro.gameObject.layer = 13;
        }
        else if (player.playertype == PlayerType.Own)
        {
            pro.gameObject.layer = 12;
        }


        pro.effectAction = (Collision collision) =>
        {
            AimObject aimObject = collision.gameObject.GetComponent<AimObject>();
            if (aimObject != null && aimObject.player.playertype != player.playertype)
            {
                AimObject gameObj = new AimObject();
                gameObj.damage = value;
                
                aimObject.Hit(gameObj);

                //스킬 버프???
                GameObject particle = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectileParticle.name);
                particle.transform.position = collision.contacts[0].point;
                particle.transform.LookAt(collision.contacts[0].normal);

                pro.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();
                pro.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObj = null;
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


    public override bool CardSelection()
    {

        var objs = GameSceneManager.Instance.ownPlayer.GetObjects<AimObject>();

        if(objs.Count > 5)
        {


            return true;

        }

        return false;
    }



}
