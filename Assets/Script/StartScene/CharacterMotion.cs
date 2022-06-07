using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
    public Animator animator;
    public Transform startPoint;
    public FireBallMove ballMove;

    public void StartGameMotion()
    {
        animator.SetBool("Attack", true);
         
    }

    public void Fire()
    {
      var go = Instantiate(ballMove);
      go.transform.position = startPoint.position;
    }

    
}
