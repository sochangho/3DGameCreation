using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryCard : MonoBehaviour
{

    public CharactorData data;
    public Image image;
    public Button button;


    public void Setting(CharactorData data, UnityAction clickAction)
    {
        this.data = data;
        image.sprite = this.data.sprite;
        button.onClick.AddListener(clickAction);
    }

}
