using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    
    private Vector3 direction;
    private Rigidbody rigidbody;

    private bool is_fire = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    private void Start()
    {
        Vector3 dir = Camera.main.transform.position - this.transform.position;
        direction = dir.normalized;

        is_fire = true;
    }

    


    private void FixedUpdate()
    {
        if (!is_fire)
        {
            return;
        }
        rigidbody.AddForce(direction * 30, ForceMode.Force);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {

            StartSceneManager.Instance.GameStart();
        }
    }


}
