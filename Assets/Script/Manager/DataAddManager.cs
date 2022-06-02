using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataAddManager : GameManager<DataAddManager>
{


   public void DataAdd(AimObject aimObject)
   {
        string pathData = File.ReadAllText(Application.dataPath + "/player.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(pathData);

        List<string> dataStrs = null;
        string dataStr = null;
        if(aimObject is Charater)
        {
            Charater charater = (Charater)aimObject;
            dataStrs = playerData.cardNames;
            dataStr = charater.data.name;
        }else if(aimObject is Effect)
        {

            Effect effect = (Effect)aimObject;

            dataStrs = playerData.cardNames;
            dataStr = effect.data.name;
        }
        else if(aimObject is Tower)
        {
            Tower tower = (Tower)aimObject;
            dataStrs = playerData.towerNames;
            dataStr = tower.data.name;

        }

       

        

        if(dataStr == null)
        {
            Debug.LogError("데이터 x");
            return;
        }

        if(dataStrs == null)
        {
            Debug.LogError("데이터 x");
            return;
        }

        string findStr = dataStrs.Find(x => x == dataStr);

        if(findStr != null)
        {
            return;
        }


        dataStrs.Add(dataStr);



        File.WriteAllText(Application.dataPath + "/player.json", JsonUtility.ToJson(playerData));
    }
    





}
