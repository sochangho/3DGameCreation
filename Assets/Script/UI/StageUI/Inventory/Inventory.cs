using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Inventory : MonoBehaviour , Iinformation
{
    public Transform cardsContent;
    public List<Transform> deckcardTransforms;
    
    public CardElement cardElementPrefab;

    public DeckCard deckCardPrefab;


    public void Awake()
    {
        InventoryLoad();
    }

    public void InventoryLoad()
    {
        string pathstr = File.ReadAllText(Application.dataPath + "/player.json");

        PlayerData playerdata = JsonUtility.FromJson<PlayerData>(pathstr);

        List<string> cardnames = playerdata.cardNames;
        List<string> effectnames = playerdata.effectNams;
        List<string>  deckcardnames  =  playerdata.cardDackNames;

        for(int i = 0; i < cardnames.Count; i++)
        {
            string pathStr = string.Format("data/{0}", cardnames[i]);
            CharactorData cardData = Resources.Load<CharactorData>(pathStr);
            CardElement element = Instantiate(cardElementPrefab);
            
            element.transform.parent = cardsContent;
            element.transform.localScale = new Vector2(1, 1.3f);
            CardElementSet(element, cardData);
            
        }

        for(int i = 0; i < effectnames.Count; i++)
        {
           
            string pathStr = string.Format("EffectData/{0}", effectnames[i]);
            CharactorData cardData = Resources.Load<CharactorData>(pathStr);
            CardElement element = Instantiate(cardElementPrefab);
            
            element.transform.parent = cardsContent;
            element.transform.localScale = new Vector2(1, 1.3f);
            CardElementSet(element, cardData);



        }

        for(int i = 0; i < deckcardnames.Count; i++)
        {
           string cardname = cardnames.Find(x => x == deckcardnames[i]);
            string effectname = effectnames.Find(x => x == deckcardnames[i]);

            if(cardname != null)
            {

                Compare(cardname);

                



                string pathStr = string.Format("data/{0}", cardname);
                CharactorData cardData = Resources.Load<CharactorData>(pathStr);
                DeckCard deck = Instantiate(deckCardPrefab);
               
                for (int j = 0; j < deckcardTransforms.Count; j++)
                {
                    if (deckcardTransforms[j].childCount == 0)
                    {
                        deck.transform.parent = deckcardTransforms[j];
                   
                        deck.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                        deck.transform.localScale = new Vector2(1, 1);
                        CardDeckSet(deck, cardData);
                        break;
                    }
              

                }
                
            }
            
            if(effectname != null)
            {

                Compare(effectname);
                string pathStr = string.Format("EffectData/{0}", effectname);
                CharactorData cardData = Resources.Load<CharactorData>(pathStr);
                DeckCard deck = Instantiate(deckCardPrefab);
                
                for (int j = 0; j < deckcardTransforms.Count; j++)
                {
                    if (deckcardTransforms[j].childCount == 0)
                    {
                       
                        deck.transform.parent = deckcardTransforms[j];
                        deck.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                        deck.transform.localScale = new Vector2(1, 1);
                        CardDeckSet(deck, cardData);
                        break;
                    }


                }
            }





        }

    }
    

    private void InventorySave()
    {
        PlayerData playerData = new PlayerData();

        for (int i = 0; i < cardsContent.childCount; i++)
        {
            CardElement element = cardsContent.GetChild(i).GetComponent<CardElement>();

            if(element.data.charater is Effect)
            {
                playerData.effectNams.Add(element.data.name);
            }
            else
            {
                playerData.cardNames.Add(element.data.name);

            }
        }

        for(int i = 0;  i < deckcardTransforms.Count; i++)
        {
            if(deckcardTransforms[i].childCount > 0)
            {
               string cardname = deckcardTransforms[i].GetChild(0).GetComponent<DeckCard>().data.name;
               playerData.cardDackNames.Add(cardname);
            }
        }

        File.WriteAllText(Application.dataPath + "/player.json", JsonUtility.ToJson(playerData));

    }



    private void CardDeckSet(InventoryCard inventoryCard, CharactorData data)
    {

        for (int i = 0; i < cardsContent.childCount; i++)
        {
            CardElement element = cardsContent.GetChild(i).GetComponent<CardElement>();

            if (element.data.name == data.name)
            {
                element.button.interactable = false;
                break;
            }

        }

        
        inventoryCard.Setting(data, () => {


            Compare(data.name);           
            Destroy(inventoryCard.gameObject);
            
        });

    }


    private void CardElementSet(InventoryCard inventoryCard , CharactorData data)
    {
        inventoryCard.Setting(data ,
            () =>
            {
               DeckCard  deckCard = Instantiate(deckCardPrefab);
                int cnt = 0;
               for(int i = 0; i < deckcardTransforms.Count; i++)
                {
                    if(deckcardTransforms[i].childCount == 0)
                    {
                       deckCard.transform.parent = deckcardTransforms[i];
                       deckCard.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

                    }
                    else
                    {
                        cnt++;
                    }

               }

               
               if(cnt == deckcardTransforms.Count)
                {
                    informationPrint("더이상 카드를 선택할 수 없습니다.");
                    Destroy(deckCard.gameObject);
                    return;
                }

                inventoryCard.button.interactable = false;
                deckCard.Setting(data, () => {


                    Compare(data.name);
                    Destroy(deckCard.gameObject);
                });


                InventorySave();

            }
            
        );


    }
    public void Compare(string cardname)
    {
        for (int i = 0; i < cardsContent.childCount; i++)
        {
            CardElement element = cardsContent.GetChild(i).GetComponent<CardElement>();

            if (element.data.name == cardname)
            {
                element.button.interactable = true;
                return;
            }

        }


    }

    public void informationPrint(string str) { }

    public void Exit()
    {
        InventorySave();
        Destroy(this.gameObject);
    }
}
