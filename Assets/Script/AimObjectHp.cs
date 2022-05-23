using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AimObjectHp : MonoBehaviour
{
    Transform cam;
    public Image image;

   
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

   
    void Update()
    {
        transform.LookAt(transform.position + cam.rotation *
            Vector3.forward, cam.rotation * Vector3.up);      
    }

    public void FillHpImg()
    {
        

    }


}
