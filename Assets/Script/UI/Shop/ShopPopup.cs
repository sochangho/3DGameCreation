
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
public class ShopPopup : MonoBehaviour
{
    public PurchaseCard purchaseCard;
    public PurchaseInfoPopup purchaseInfoPopup;
    public Transform content;

    public Button button;

    public TextMeshProUGUI goldText;

    public void Start()
    {
        LoadCards();
        SetPlayerGoldText();
        button.onClick.AddListener(Exit);
    }

    public void Exit()
    {
        FindObjectOfType<CardPurchase>().ShopPopupClose();
        Destroy(this.gameObject);
    }

    public void LoadCards()
    {
        string pathData = File.ReadAllText(Application.dataPath + "/player.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(pathData);

        List<string> characterNames = playerData.cardNames;
        List<string> effectNames = playerData.effectNams;


        CharactorData[] charactorDatas = Resources.LoadAll<CharactorData>("data");
        EffectData[] effectDatas = Resources.LoadAll<EffectData>("EffectData");


        for(int i = 0; i < charactorDatas.Length; i++)
        {
           PurchaseCard pgo =  Instantiate(purchaseCard);
           pgo.transform.parent = content;

           pgo.Setting(charactorDatas[i], ()=> { OpenPopup(pgo);  });

           string findname =  characterNames.Find(x => x == charactorDatas[i].name);

           if(findname != null)
            {
                pgo.button.interactable = false;
                pgo.Blind();
            }


        }

        for (int i = 0; i < effectDatas.Length; i++)
        {
            PurchaseCard pgo = Instantiate(purchaseCard);
            pgo.transform.parent = content;

            pgo.Setting(effectDatas[i], () => { OpenPopup(pgo); });


            string findname = effectNames.Find(x => x == effectDatas[i].name);

            if (findname != null)
            {
                pgo.button.interactable = false;
                pgo.Blind();
            }

        }

    }

    private void OpenPopup(PurchaseCard purchaseCard)
    {
         if(FindObjectOfType<PurchaseInfoPopup>() != null)
        {

            return;
        }


        var go  = Instantiate(purchaseInfoPopup);
        go.transform.parent = this.transform;
        go.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        go.Open(purchaseCard);


         
    }

 
    public void SetPlayerGoldText()
    {
        int gold = PlayerPrefs.GetInt("gold");

        goldText.text = gold.ToString();

    }


}
