using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tower : AimObject
{

    public TowerState towerState;
    public Transform rotationHead;

    public Detect detect;
    public Attack attack;

    public float attackdelayTime = 0.3f;
    private float curTime = 0;

    public TowerData data;


    public float AttackDelayTime
    {
        get
        {
            float v = attackdelayTime;
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


   override public void Awake()
    {
        TowerInit();
        base.Awake();
        detect = new SphereOverlapDetect(this, range);
        attack = GetComponent<Attack>();
        attack.init(this);
      
    }




    override public void Attack()
    {
        if (attackTarget == null)
        {
            return;
        }


        attack.AttackTarget(attackTarget);
    }



    public override void Hit(AimObject damage)
    {
        if (damage == null)
        {
            return;
        }



        cur_hp -= (damage.Damage - damage.Damage * Defence / 100);

        base.Hit(damage);
        if (cur_hp <= 0)
        {

            cur_hp = 0;


            if(towerState != TowerState.Die)
            {
                towerState = TowerState.Die;
                player.RemoveCharacter(this);
                damage.attackTarget = null;
                Die();
            }       
        }
    }

    public override void Die()
    {
        GameSceneManager.Instance.ownPlayer.gameStart = false;
        GameSceneManager.Instance.oponentPlayer.gameStart = false;
        if (this.player.playertype == PlayerType.Oponent)
        {

          
            GameSceneManager.Instance.stateUi.Win(()=> {

                GameSceneManager.Instance.SceneTransition();

                DataAddManager.Instance.DataAdd(this);

                int gold = PlayerPrefs.GetInt("gold");
                gold += 1000;
                PlayerPrefs.SetInt("gold", gold);

            });

        }
        else if(this.player.playertype == PlayerType.Own)
        {
            // 패배
            // 처음으로 되돌아간다.
            // 게임씬매니저의 함수 호출

            GameSceneManager.Instance.stateUi.GameOver(() => {
                PlayerPrefs.SetInt("playersave", 0);

                GameSceneManager.Instance.SceneTransition();

            });
            
        }
    }


    private void TowerInit()
    {

        if (data != null)
        {
            hp = data.hp;
            damage = data.damage;
            range = data.range;
            defence = data.defence;
            attackdelayTime = data.AttackDelayTime;
        }

        cur_hp = hp;
    }

    private void Update()
    {
        buffController.BuffTimer();
        if (towerState == TowerState.Detect)
        {

            detect.OnDetect();

            if (attackTarget != null)
            {
                towerState = TowerState.Attack;
                curTime = Time.time;
            }


        }
        else if (towerState == TowerState.Attack)
        {

           

            if (attackTarget == null)
            {
                towerState = TowerState.Detect;
            }
            else
            {

                Vector3 lookPos = new Vector3(attackTarget.transform.position.x, rotationHead.transform.position.y, attackTarget.transform.position.z);

                rotationHead.LookAt(lookPos);

                if (Time.time < curTime + AttackDelayTime)
                {
                    
                    
                    return;
                }
               
                curTime = Time.time;
                Attack();
            }

        }
        else if (towerState == TowerState.Die)
        {

            // StopAI();

        }
    }






}
