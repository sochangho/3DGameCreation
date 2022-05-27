using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageMapUI : MonoBehaviour
{

    public MapLevel mapLevel;
    public Transform content;


    void Start()
    {
        EventManager.On("MapfirstInit", StageManager.Instance.MakeinitStage);
        //EventManager.On("MapfirstInit", StageManager.Instance.DataSaveNode);
        
        //EventManager.On("MapfirstInit", StageManager.Instance.FindZerochildLink);
        //EventManager.On("MapfirstInit", StageManager.Instance.Show);

        MapUICreate();
    }

    
    public void MapUICreate()
    {
        EventManager.Emit("MapfirstInit", null);
      

       
         MapLevel maplevel = Instantiate(mapLevel);         
         maplevel.transform.parent = content;
         maplevel.layoutGroup = content.GetComponent<GridLayoutGroup>();
         maplevel.levelButtonSetting();
         




    }



}
