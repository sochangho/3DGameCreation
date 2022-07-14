using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeTriggerEvent : MonoBehaviour
{
    private EventTrigger eventTrigger;

    private Nodedatas nodedatas;

    public void Start()
    {
        StageNode<TemporaryData> nInfo = GetComponent<NodeButton>().nodeInfo;
        nodedatas = (Nodedatas)nInfo.data.obj;

        if(nodedatas.state == StageNodeState.Shop)
        {
            return;
        }

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

    private void OnEnterViewDatas(PointerEventData data)
    {
        if (StageManager.Instance.buttonState == StageButtonState.View)
        {
            return;
        }


        //StageNode<TemporaryData> nInfo = GetComponent<NodeButton>().nodeInfo;

       
        //Nodedatas nodedatas = (Nodedatas)nInfo.data.obj;



        List<CharactorData> charactorDatas = nodedatas.GetCardDatas<CharactorData>();
        List<EffectData> effectDatas = nodedatas.GetCardDatas<EffectData>();

        List<KeyValuePair<string, Sprite>> datainfos = new List<KeyValuePair<string, Sprite>>();
        foreach (CharactorData charactorData in charactorDatas)
        {
            KeyValuePair<string, Sprite> keyValue
                = new KeyValuePair<string, Sprite>(charactorData.characterName, charactorData.sprite);

            string find = datainfos.Find(x => x.Key == keyValue.Key).Key;

            if (find != null)
            {
                continue;
            }


            datainfos.Add(keyValue);
        }

        foreach (EffectData effectData in effectDatas)
        {
            KeyValuePair<string, Sprite> keyValue
                = new KeyValuePair<string, Sprite>(effectData.effectName, effectData.sprite);

            string find = datainfos.Find(x => x.Key == keyValue.Key).Key;

            if (find != null)
            {
                continue;
            }

            datainfos.Add(keyValue);
        }
        EventManager.Emit("ViewInfos", datainfos);
    }

    private void OnExitViewDatas(PointerEventData data)
    {
        if (StageManager.Instance.buttonState == StageButtonState.Hiden)
        {
            return;
        }


        EventManager.Emit("ViewHiden", null);

    }
}
