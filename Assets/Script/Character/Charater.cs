using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
public class Charater : AimObject , IAttacked 
{
 
    public float speed;
    
    public Transform rayPoint;
 
   
    private Detect detect;
    private Attack attack;

    private NavMeshAgent playerNav;
    private Animator animator;
    private Coroutine characterRoutin;
    
    public int ID { get; set; }

    

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
        detect = GetComponent<Detect>();
        attack = GetComponent<Attack>();
        playerNav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if(detect == null)
        {

            Debug.LogError("탐지 컴포넌트 null");
        }
        if (attack == null)
        {

            Debug.LogError("공격 컴포넌트 null");
        }
        if(playerNav == null)
        {
            Debug.LogError("네브 메쉬 컴포넌트 null");
        }

        detect.init(this);
        attack.init(this);
        playerNav.speed = Speed;

    }

    public void Start()
    {      
       characterRoutin =  StartCoroutine(CharacterRoutin());

        cur_hp = hp;
        Debug.Log("max 체력 :: " + cur_hp);
        Debug.Log("초기 체력 :: " + cur_hp);

    }



    public void Hit(AimObject attackCha)
    {
        if(attackCha == null)
        {
            return;
        }


        cur_hp -= (attackCha.Damage - attackCha.Damage * Defence / 100);

        
        if(cur_hp <= 0)
        {
           
            cur_hp = 0;
           
            player.RemoveCharacter(this);
            attackCha.attackTarget = null;
            StopCoroutine(characterRoutin);
            OnDieAni();
            Die();
           
        }
    }

    public void Attack()
    {
        if(attackTarget == null)
        {
            return;
        }


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
                    detect.OnDetect();
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

                    if (FindMonsterRay() == null)
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

                if (FindMonsterRay() == null)
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

    private Collider FindMonsterRay()
    {

       

        RaycastHit hit;
        if(Physics.Raycast(rayPoint.position , transform.forward , out hit, range, LayerMask.GetMask("Monster")))
        {
           
            return hit.collider;

        }

       
        return null;
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
