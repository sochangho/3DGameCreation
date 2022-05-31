using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeContent : MonoBehaviour
{
   public GameObject cardListGameObject;
   public GameObject deckListGameObject;
   public GameObject towerListGameObject;
   public Button cardsButton;
   public Button towersButton;



    void Start()
    {

        cardsButton.onClick.AddListener(ShowCards);
        towersButton.onClick.AddListener(ShowTowers);

    }

    public void ShowCards()
    {
        cardsButton.interactable = false;
        towersButton.interactable = true;
        cardListGameObject.SetActive(true);
        deckListGameObject.SetActive(true);
        towerListGameObject.SetActive(false);

    }

    public void ShowTowers()
    {
        cardsButton.interactable = true;
        towersButton.interactable = false;
        cardListGameObject.SetActive(false);
        deckListGameObject.SetActive(false);
        towerListGameObject.SetActive(true);
        
    }
   

}
