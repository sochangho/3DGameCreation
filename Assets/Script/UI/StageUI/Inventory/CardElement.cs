

using UnityEngine.EventSystems;
using UnityEngine;
public class CardElement : InventoryCard 
{
    public GameObject infoObject;

    public GameObject effectinfoObject;

    private EventTrigger eventTrigger;
    protected GameObject infoObjectClone;
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


    virtual protected void CreateInfo()
    {

        GameObject info = null;
      if(data is CharactorData)
      {
          info =  Instantiate(infoObject);
          
      }
      else if(data is EffectData)
      {

           info = Instantiate(effectinfoObject);
      }


        info.transform.parent = config.infoCanvas.transform;
        info.GetComponent<RectTransform>().position = Input.mousePosition;
        infoObjectClone = info;


        var information =infoObjectClone.GetComponent<CardDiscription>();
        information.Set(data);

    }
}
