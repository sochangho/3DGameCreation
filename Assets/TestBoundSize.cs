using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoundSize : MonoBehaviour
{
    MeshRenderer meshrender;



    // Start is called before the first frame update
    void Start()
    {


        meshrender = GetComponent<MeshRenderer>();

       Vector3 siz = meshrender.bounds.size;


        Debug.Log(siz.x + " , " + siz.y + " , " + siz.z);


    }

}
