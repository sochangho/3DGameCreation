using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class StageSceneConfig : MonoBehaviour
{
    public Inventory inventory;
    public Canvas canvas;
    public List<CharactorData> datas;

    private void Awake()
    {

        EventManager.On("MakeScene", StageManager.Instance.MakeinitStage);
        EventManager.On("MakeScene", PlayerDataSetInit);
        EventManager.On("LoadScene", StageManager.Instance.DataLoadNode);


        if (!PlayerPrefs.HasKey("playersave"))
        {
            PlayerPrefs.SetInt("playersave", 0);
            PlayerPrefs.SetInt("playerlevel", 0);
            PlayerPrefs.SetInt("playerindex", 0);
            PlayerPrefs.SetInt("gold", 0);

            EventManager.Emit("MakeScene", null);

        }
        else
        {
            EventManager.Emit("LoadScene", null);
        }



    }





    private void PlayerDataSetInit(object parameter)
    {
        PlayerPrefs.SetInt("playersave", 1);
        
        PlayerData playerData = new PlayerData();
   

        foreach (CharactorData data in datas)
        {
            if (data.charater is Effect)
            {

                playerData.effectNams.Add(data.name);
            }
            else
            {
                playerData.cardNames.Add(data.name);
            }
        }

        File.WriteAllText(Application.dataPath + "/player.json", JsonUtility.ToJson(playerData));
    }

    public void InventoryButtonClick()
    {

       Inventory go =  Instantiate(inventory);
       go.transform.parent = canvas.transform;
       go.GetComponent<RectTransform>().localPosition = new Vector2(-6.654907f, -11f);
    }


}
