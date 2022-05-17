using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
public class Charater : AimObject 
{
 
    public float speed;    
    private Attack attack;
    private NavMeshAgent playerNav;
    private Animator animator;

    public CharactorData data;
    public float Speed
    {

        get
        {
            float v = speed;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is SeepdBuff)
                {

                    v += buff.value;
                }
            }
              
            return v;
        }

    }

    public CharaterState state;
   



    public void Awake()
    {
        
        attack = GetComponent<Attack>();
        playerNav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
       
        if (attack == null)
        {

            Debug.LogError("공격 컴포넌트 null");
        }
        if(playerNav == null)
        {
            Debug.LogError("네브 메쉬 컴포넌트 null");
        }

 
        attack.init(this);
        playerNav.speed = Speed;




    }

    public void Start()
    {      
       aiRoutin =  StartCoroutine(CharacterRoutin());
       CharacterInit();
        cur_hp = hp;
        Debug.Log("max 체력 :: " + cur_hp);
        Debug.Log("초기 체력 :: " + cur_hp);

    }

    public void CharacterInit()
    {
        if (data != null)
        {
            hp = data.hp;
            damage = data.damage;
            range = data.range;
            defence = data.defence;
            speed = data.speed;
        }



    }

    override public void Hit(AimObject attackCha)
    {
        if(attackCha == null)
        {
            return;
        }


        cur_hp -= (attackCha.Damage - attackCha.Damage * Defence / 100);

        
        if(cur_hp <= 0)
        {
           
            cur_hp = 0;


            if (state != CharaterState.Die)
            {
                state = CharaterState.Die;
                player.RemoveCharacter(this);
                attackCha.attackTarget = null;

                if (aiRoutin != null)
                {

                    StopAI();

                }
                OnDieAni();
                Die();
            }
        }
    }

    override public void Attack()
    {
        if(attackTarget == null)
        {
            return;
        }


        Vector3 pos = attackTarget.transform.position;

        pos.y = transform.position.y;

        transform.LookAt(pos);


        attack.AttackTarget(attackTarget);
       
    }


    IEnumerator CharacterRoutin()
    {
        while (true)
        {
            if(state == CharaterState.Detect)
            {
                

                if (attackTarget != null)
                {
                    state = CharaterState.Trace;
                    OnWalkAni();
                }
                else
                {
                   attack.detect.OnDetect();
                }

            }
            else if(state == CharaterState.Trace)
            {
                if (attackTarget == null)
                {
                    state = CharaterState.Detect;
                    OnIdleAni();
                }
                else
                {


                    Vector3 cVec3;
                    cVec3.x = transform.position.x;
                    cVec3.y = 0;
                    cVec3.z = transform.position.z;



                    Vector3 tVec3;
                    tVec3.x = attackTarget.gameObject.transform.position.x;
                    tVec3.y = 0;
                    tVec3.z = attackTarget.gameObject.transform.position.z;

                    if (!FindOverlapSphere())
                    {
                        playerNav.speed = Speed;
                        playerNav.SetDestination(new Vector3(attackTarget.gameObject.transform.position.x,
                            transform.position.y
                            , attackTarget.gameObject.transform.position.z));
                    }
                    else
                    {
                        OnAttackAni();
                        playerNav.speed = 0;
                        state = CharaterState.Attack;

                       
                    }
                }
        
            }
            else if(state == CharaterState.Attack)
            {

                if (!FindOverlapSphere())
                {
                    state = CharaterState.Detect;
                    OnIdleAni();

                   
                }
                else
                {

                    if (attackTarget != null)
                    {
                        transform.LookAt(attackTarget.transform);
                    }
                    else
                    {
                        state = CharaterState.Detect;
                        OnIdleAni();
                    }
                }

            }

            yield return null;
        }


    }

    

 


    private void OnWalkAni()
    {
        if (!animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", true);
        }

        if (animator.GetBool("Attack"))
        {
            animator.SetBool("Attack", false);
        }
    }

    private void OnAttackAni()
    {
        if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        if (!animator.GetBool("Attack"))
        {
            animator.SetBool("Attack", true);
        }

    }

    private void OnIdleAni()
    {

        if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        if (animator.GetBool("Attack"))
        {
            animator.SetBool("Attack", false);
        }

    }

    private void OnDieAni()
    {
        animator.SetTrigger("Die");

        if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        if (animator.GetBool("Attack"))
        {
            animator.SetBool("Attack", false);
        }

    }



    private bool FindOverlapSphere()
    {

        if(attackTarget == null)
        {
            return false;
        }
        

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

  
        foreach(Collider collider in colliders)
        {

            AimObject aimobj = collider.GetComponent<AimObject>();
            if(aimobj != null && aimobj.player.playertype != player.playertype && attackTarget.ID == aimobj.ID)
            {
                return true;
                
            }

        }

        return false;

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(transform.position, range);
    }


    public void Die()
    {
        StartCoroutine(DieRoutin());
    }

    IEnumerator DieRoutin()
    {

        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }


}
