using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentCardsInfo : MonoBehaviour
{
    [SerializeField]
    private OponentCardsInfoEntry entry;

    [SerializeField]
    private Transform parentTransform;

    private List<OponentCardsInfoEntry> oponentCardsInfoEntries = new List<OponentCardsInfoEntry>();

    private Animator entrysAnimator;

     
    private void Start()
    {
        entrysAnimator = GetComponent<Animator>();

        EventManager.On("ViewInfos", ViewEvent);
        EventManager.On("ViewHiden", HidenEvent);
    }


    private void ViewEvent(object parameter)
    {
        List <KeyValuePair<string, Sprite>> infos = (List<KeyValuePair<string, Sprite>>)parameter;
        ViewEntrys(infos);
    }

    private void HidenEvent(object parmeter)
    {
        HideEntrys();
    }

    public void ViewEntrys(List<KeyValuePair<string ,Sprite>> infos)
    {
        for (int i = 0; i < oponentCardsInfoEntries.Count; i++)
        {
            Destroy(oponentCardsInfoEntries[i].gameObject);
        }

        oponentCardsInfoEntries.Clear();


        foreach (KeyValuePair<string, Sprite> info in infos)
        {
           OponentCardsInfoEntry eog = Instantiate(entry);
           eog.transform.parent = parentTransform;
           eog.GetComponent<RectTransform>().localScale = Vector2.one;
           eog.CardInfoSet(info.Value, info.Key);
           oponentCardsInfoEntries.Add(eog);
        }
        entrysAnimator.SetBool("Create", true);
        entrysAnimator.SetBool("Hiden", false);

        
    } 

    public void HideEntrys()
    {
      
        //entrysAnimator.SetBool("Create", true);
        entrysAnimator.SetBool("Hiden",  true);
    }


    public void ViewAfter()
    {
        StageManager.Instance.buttonState = StageButtonState.View;
    }

    public void HidenAfter()
    {
        //for (int i = 0; i < oponentCardsInfoEntries.Count; i++)
        //{
        //    Destroy(oponentCardsInfoEntries[i].gameObject);
        //}

        //oponentCardsInfoEntries.Clear();

        StageManager.Instance.buttonState = StageButtonState.Hiden;
    }



}
