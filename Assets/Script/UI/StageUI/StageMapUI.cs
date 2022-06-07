using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageMapUI : MonoBehaviour
{

    public MapLevel mapLevel;
    public Transform content;
    public GameObject scrollView;

    public MapLevel cloneMapLevel;

    void Start()
    {
        

        
       

        MapUICreate();
    }

    
    public void MapUICreate()
    {
       // EventManager.Emit("MapfirstInit", null);
      

       
         MapLevel maplevel = Instantiate(mapLevel);         
         maplevel.transform.parent = content;
         maplevel.layoutGroup = content.GetComponent<GridLayoutGroup>();
         float xSize = scrollView.GetComponent<RectTransform>().rect.width;
         maplevel.layoutGroup.cellSize = new Vector2(xSize, maplevel.layoutGroup.cellSize.y);
         maplevel.levelButtonSetting();

         cloneMapLevel = maplevel;
    }



}
