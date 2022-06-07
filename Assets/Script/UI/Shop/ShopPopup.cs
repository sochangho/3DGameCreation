using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ShopPopup : MonoBehaviour
{
    public PurchaseCard purchaseCard;
    public PurchaseInfoPopup purchaseInfoPopup;
    public Transform content;

    public void Start()
    {
        LoadCards();
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

           pgo.Setting(charactorDatas[i], ()=> {   });

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

            pgo.Setting(effectDatas[i], () => {  });


            string findname = effectNames.Find(x => x == effectDatas[i].name);

            if (findname != null)
            {
                pgo.button.interactable = false;
                pgo.Blind();
            }

        }






    }

  


}
