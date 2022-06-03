using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class TowerCard : MonoBehaviour
{
    public TowerData data;
    public Image image;
    public Button button;
    public GameObject selectObj;

    public void Setting(TowerData data)
    {
        this.data = data;
        image.sprite = this.data.sprite;

        button.onClick.AddListener(ButtonAddEvent);
    }

    public void ButtonAddEvent()
    {
        Transform p = this.transform.parent;

        button.interactable = false;
        selectObj.SetActive(true);
        FindObjectOfType<Inventory>().useTowerDataName = data.name;
        FindObjectOfType<Inventory>().towerCardInfo.Set(data);
        for (int i = 0; i < p.childCount; i++)
        {
            if(data.name != p.GetChild(i).GetComponent<TowerCard>().data.name)
            {
                p.GetChild(i).GetComponent<TowerCard>().selectObj.SetActive(false);
                p.GetChild(i).GetComponent<Button>().interactable = true;
            }


        }



    }


}
