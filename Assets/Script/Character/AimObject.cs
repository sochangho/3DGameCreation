using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AimObject : MonoBehaviour,IAttacked,IObjectInfo
{
    
    public AimObject attackTarget;
   
    public Player player;
    public float hp;
    public float cur_hp = 100;
    public float damage;
    public float defence;
    public float range;
    public int ID { get; set; }

    public GameObjType type;


    public Coroutine aiRoutin;

    readonly public BuffController buffController = new BuffController();

    public AimObjectHp aimObjectHp;

   virtual public void Awake()
   {
        aimObjectHp = GetComponentInChildren<AimObjectHp>();
        
        if(aimObjectHp != null)
        {
            aimObjectHp.FillHpImg(this);

        }
   }

    public float Defence
    {
        get
        {
            float v = defence;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is DefenceBuff)
                {

                    v += buff.value;
                }
            }
            return v;
        }
    }

    public float Damage
    {
        get
        {
            float v = damage;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is DamageBuff)
                {

                    v += buff.value;
                }
            }
            return v;
        }



    }




    public float Range
    {

        get
        {
            float v = range;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is AttackDelayBuff)
                {

                    v += buff.value;
                }
            }
            return v;
        }


    }


    public void StopAI()
    {
        if (aiRoutin != null)
        {

            StopCoroutine(aiRoutin);
        }
    }

    virtual public void Attack() { }
    virtual public void Hit(AimObject damage){ HpImgSet();  }
    virtual public void Die() { }

    public void HpImgSet()
    {
        if (aimObjectHp != null)
        {
            aimObjectHp.FillHpImg(this);

        }

    }

    public Image oponentFill;
    public Image playerFill;

    public void playerTypeFill()
    {

        if (player.playertype == PlayerType.Own)
        {
            oponentFill.gameObject.SetActive(false);
            aimObjectHp.image = (Image)playerFill;
        }
        else
        {

            playerFill.gameObject.SetActive(false);
            aimObjectHp.image = (Image)oponentFill;
        }

    }

    public PlayerType AttackedObjectType()
    {

        return player.playertype;
    }

    public float GetDamage()
    {

        return damage;
    }
    public float GetHp()
    {
        return hp;

    }
}
