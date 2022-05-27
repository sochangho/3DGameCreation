using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MapNodesData
{
   public int type;
   public int level;
   public int index;
   public List<string> monsterNames;
   public List<int> parentIndex;
   public List<int> childeIndex;
}

[System.Serializable]
public class MaptotalData
{
   public List<MapNodesData> mapNodesDatas;


}