using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerayDataManager : GameManager<RerayDataManager>
{

    public List<CharactorData> charactorDatas = new List<CharactorData>();
    public List<EffectData> effectDatas = new List<EffectData>();
    public TowerData towerData;

    public int Currentlevel { get; set; }
    public int Currentindex { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void DataAdd(List<ScriptableObject> datas , TowerData data)
    {
        charactorDatas.Clear();
        effectDatas.Clear();

        for(int i = 0; i < datas.Count; i++)
        {
            if(datas[i] is CharactorData)
            {
                CharactorData charactorData = (CharactorData)datas[i];
                charactorDatas.Add(charactorData);
            }
            else if (datas[i] is EffectData)
            {

                EffectData effectData = (EffectData)datas[i];

                effectDatas.Add(effectData);

            }
        }


        towerData = data;

    }




}
