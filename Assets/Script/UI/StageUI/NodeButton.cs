using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


using UnityEngine.SceneManagement;

public class NodeButton : MonoBehaviour
{

    public List<SceneData> sceneDatas;
    private Button button;
    private Image image;
    private string sceneName;
    private EventTrigger eventTrigger;
    public StageNode<TemporaryData> nodeInfo { get; set; }

    public void SceneInit(StageNodeState state)
    {
        foreach(SceneData data in sceneDatas)
        {
            if(data.state == state)
            {
                sceneName = data.scene;

                if (image == null)
                {
                    image = GetComponent<Image>();
                }
                image.sprite = data.icon;

            }
        }


        if(button == null)
        {
            button = GetComponent<Button>();
        }

        if(image == null)
        {
            image = GetComponent<Image>();
        }
        button.onClick.AddListener(OnClickMove);
    }

    private void OnClickMove()
    {

        OnClickNode();
    }

   


    private void OnClickNode()
    {

        //정보전달
       RerayDataManager dataManager = RerayDataManager.Instance;


       Nodedatas nodedatas = (Nodedatas)nodeInfo.data.obj;

        if (nodedatas.state != StageNodeState.Shop)
        {
            List<ScriptableObject> datas = nodedatas.datas;

            TowerData[] towerDatas = Resources.LoadAll<TowerData>("TowerData");




            if(towerDatas.Length == 0)
            {
                Debug.LogError("타워 데이터 X");
                return;
            }            
            int random = Random.Range(0, towerDatas.Length);
            dataManager.DataAdd(datas, towerDatas[random]);

        }



        dataManager.Currentindex = nodeInfo.level;
        dataManager.Currentlevel = nodeInfo.index;

     
       
       SceneManager.LoadScene(sceneName);

    }



}

[System.Serializable]
public class SceneData
{
    public string scene;
    public Sprite icon;
    public StageNodeState state;
}

