using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStage : MonoBehaviour
{
   public GameObject nodePrefab;
   public Transform startTransform;
 
   public LineRenderer lineRenderer;

   
    // Start is called before the first frame update
    void Start()
    {
        StageManager.Instance.startTransform = startTransform;
        StageManager.Instance.nodePrefab = nodePrefab;
        StageManager.Instance.line = lineRenderer;
        StageManager.Instance.MakeinitStage();
        StageManager.Instance.Show();
        StageManager.Instance.FindZerochildLink();
    }

   

 
}
