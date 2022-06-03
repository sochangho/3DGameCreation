using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardInfo : MonoBehaviour
{
    public int id;
    public Image icon;
    public int cost;
    public Text costText;
    public Card card;
    public GameObject blind;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectCard);
        
    }

    
    public void SelectCard()
    {
        if (GameSceneManager.Instance.spwanObjet == null)
        {

            ParameterHelper playerParameter = new ParameterHelper();
            playerParameter.objList.Add(GameSceneManager.Instance.ownPlayer);
            playerParameter.objList.Add(this);

            EventManager.Emit("CardSelect", playerParameter);
        }
    }

    public void SetButtonPressable(bool value)
    {

        if (!value)
        {
            button.interactable = false;
            blind.SetActive(true);
        }
        else
        {
            button.interactable = true;
            blind.SetActive(false);

        }

    }

}
