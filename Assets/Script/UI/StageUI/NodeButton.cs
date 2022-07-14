using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


using UnityEngine.SceneManagement;

public class NodeButton : MonoBehaviour
{
    public InfoCreater infoCreater;
    public List<SceneData> sceneDatas;
    private Button button;
    private Image image;
    private string sceneName;
    private EventTrigger eventTrigger;
    public StageNode<TemporaryData> nodeInfo { get; set; }

    public void SceneInit(StageNodeState state)
    {
        foreach (SceneData data in sceneDatas)
        {
            if (data.state == state)
            {
                sceneName = data.scene;

                if (image == null)
                {
                    image = GetComponent<Image>();
                }
                image.sprite = data.icon;

            }
        }


        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (image == null)
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

        //��������
        RerayDataManager dataManager = RerayDataManager.Instance;


        Nodedatas nodedatas = (Nodedatas)nodeInfo.data.obj;

        if (nodedatas.state != StageNodeState.Shop)
        {


            int cnt = StageManager.Instance.DeckCardsCnt();

            if (cnt < 5)
            {


                InfoCreater obj = Instantiate(infoCreater);
                obj.transform.parent = FindObjectOfType<StageSceneConfig>().canvas.transform;
                obj.GetComponent<RectTransform>().localScale = Vector2.one;
                obj.GetComponent<RectTransform>().localPosition = Vector2.zero;
                obj.infoText.text = "5개의 카드를 골라주세요.";


                return;
            }

            List<ScriptableObject> datas = nodedatas.datas;

            TowerData[] towerDatas = Resources.LoadAll<TowerData>("TowerData");




            if (towerDatas.Length == 0)
            {
                Debug.LogError("Ÿ�� ������ X");
                return;
            }
            int random = Random.Range(0, towerDatas.Length);
            dataManager.DataAdd(datas, towerDatas[random]);

            dataManager.Currentindex = nodeInfo.index;
            dataManager.Currentlevel = nodeInfo.level;

      
        }
        else
        {

            PlayerPrefs.SetInt("playerlevel", nodeInfo.level);
            PlayerPrefs.SetInt("playerindex", nodeInfo.index);

            StageManager.Instance
            .ClearChange(nodeInfo.level
            , nodeInfo.index);
        }




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

