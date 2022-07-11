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

    public void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { OnEnterViewDatas((PointerEventData)data); });

        EventTrigger.Entry existEntry = new EventTrigger.Entry();
        existEntry.eventID = EventTriggerType.PointerExit;
        existEntry.callback.AddListener((data) => { OnExitViewDatas((PointerEventData)data); });

        eventTrigger.triggers.Add(enterEntry);
        eventTrigger.triggers.Add(existEntry);

    }
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

    private void OnEnterViewDatas(PointerEventData data)
    {
        Nodedatas nodedatas = (Nodedatas)nodeInfo.data.obj;
        List<CharactorData> charactorDatas= nodedatas.GetCardDatas<CharactorData>();
        List<EffectData> effectDatas = nodedatas.GetCardDatas<EffectData>();

    }

    private void OnExitViewDatas(PointerEventData data)
    {


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


       PlayerPrefs.SetInt("playerlevel" , nodeInfo.level);
       PlayerPrefs.SetInt("playerindex" , nodeInfo.index);

       StageManager.Instance.ClearChange(nodeInfo.level, nodeInfo.index);
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