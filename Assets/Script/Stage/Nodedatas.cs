using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StageNodeState
{
    Battle,
    Shop,
    Size
}


public class Nodedatas
{
    public List<ScriptableObject> datas = new List<ScriptableObject>();
    public StageNodeState state;

    public void RandomNodeData()
    {
        int randomState = Random.Range(0, 100);

        if(randomState < 30)
        {
            state = StageNodeState.Shop;
        }
        else
        {
            state = StageNodeState.Battle;

        }

        if(state == StageNodeState.Shop)
        {
            return;
        }

        CharactorData[] charactorDatas = Resources.LoadAll<CharactorData>("data");
        EffectData[] effectDatas = Resources.LoadAll<EffectData>("EffectData");
        if(charactorDatas.Length == 0)
        {
            return;
        }

        int randomCnt = Random.Range(5, 6);

        for (int i = 0; i < randomCnt; i++)
        {
            int randomIndex = Random.Range(0, charactorDatas.Length);

            datas.Add(charactorDatas[randomIndex]);

        }

        randomCnt = Random.Range(3, 5);

        for(int i = 0; i < randomCnt; i++)
        {
            int randomIndex = Random.Range(0, effectDatas.Length);
            datas.Add(effectDatas[randomIndex]);

        }



    }


}
