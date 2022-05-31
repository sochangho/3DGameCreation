using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerayDataManager : GameManager<RerayDataManager>
{

    public List<CharactorData> charactorDatas = new List<CharactorData>();
    public TowerData towerData;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void DataAdd(List<CharactorData> datas , TowerData data)
    {

        charactorDatas.AddRange(datas);
        towerData = data;

    }




}
