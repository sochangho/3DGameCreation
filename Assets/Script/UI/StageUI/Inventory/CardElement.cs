

using UnityEngine.EventSystems;
using UnityEngine;
public class CardElement : InventoryCard 
{
    public GameObject infoObject;
    private EventTrigger eventTrigger;
    private GameObject infoObjectClone;
    private StageSceneConfig config;
    private bool is_enter;

    void Start()
    {
        config = FindObjectOfType<StageSceneConfig>();
        eventTrigger = image.GetComponent<EventTrigger>();
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { OnPointerEnterInfo((PointerEventData)data); });

        EventTrigger.Entry existEntry = new EventTrigger.Entry();
        existEntry.eventID = EventTriggerType.PointerExit;
        existEntry.callback.AddListener((data) => { OnPointerExitInfo((PointerEventData)data); });

        eventTrigger.triggers.Add(enterEntry);
        eventTrigger.triggers.Add(existEntry);

    }


    public void OnPointerEnterInfo(PointerEventData data)
    {
        if (!is_enter)
        {
           var info  =  Instantiate(infoObject);
           info.transform.parent = config.infoCanvas.transform;
          
           info.GetComponent<RectTransform>().position = Input.mousePosition; 
           infoObjectClone = info;
            CreateInfo();
           is_enter = true; 
        }

    }

    public void OnPointerExitInfo(PointerEventData data)
    {
        if (is_enter)
        {
            
            is_enter = false;

            if (infoObjectClone != null)
            {
                Destroy(infoObjectClone);
            }
        }


    }


    private void CreateInfo()
    {
      var information =  infoObjectClone.GetComponent<CardDiscription>();
      information.Set(data.name, data.subscript);

    }
}
