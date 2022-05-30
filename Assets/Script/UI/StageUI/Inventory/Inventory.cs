using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform cardsContent;
    public List<Transform> deckcardTransforms;
    
    public CardElement cardElementPrefab;

    public DeckCard deckCardPrefab;


    public void InventoryLoad()
    {

    } 


    private void CardElementSet(InventoryCard inventoryCard , CharactorData data)
    {
        inventoryCard.Setting(data ,
            () =>
            {


            }
            
            );


    }

    



}
