using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    public LineRenderer line;
    public Material oponentMaterial;
   


    // Start is called before the first frame update
    void Start()
    {
          
    }

   public void CreatePoints(float radius)
    {
       
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        float x;
        float y;
        float z = 0f;

        float angle = 20f;
        for(int i = 0; i <(segments + 1); i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / segments);
        }

    }
}
