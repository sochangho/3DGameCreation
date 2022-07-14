using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageManager : GameManager<StageManager>
{

    StageNode<TemporaryData> root;

    readonly public Dictionary<int, List<StageNode<TemporaryData>>> nodesDic = new Dictionary<int, List<StageNode<TemporaryData>>>();

    public int nodelevel { get; set; } = 0;
    public int playerlevel { get; set; }
    public int playerindex { get; set; }

    int maxlevel = 7;
    int maxNodeCnt = 3;

    public StageButtonState buttonState = StageButtonState.Hiden;


    public void DataSaveNode(object parameter)
    {
        MaptotalData maptotalData = new MaptotalData();
        maptotalData.mapNodesDatas = new List<MapNodesData>();

        for (int i = 0; i < nodesDic.Count; i++)
        {
            for (int j = 0; j < nodesDic[i].Count; j++)
            {
                MapNodesData mapNodesData = new MapNodesData();
                Nodedatas nodedatas = (Nodedatas)nodesDic[i][j].data.obj;
                mapNodesData.level = nodesDic[i][j].level;
                mapNodesData.index = nodesDic[i][j].index;
                mapNodesData.type = (int)nodedatas.state;
                mapNodesData.clear = (int)nodesDic[i][j].clear;
                mapNodesData.parentIndex = new List<int>();
                mapNodesData.childeIndex = new List<int>();
                mapNodesData.monsterNames = new List<string>();
                mapNodesData.effectNames = new List<string>();

                for (int parent = 0; parent < nodesDic[i][j].parent.Count; parent++)
                {

                    mapNodesData.parentIndex.Add(nodesDic[i][j].parent[parent].index);

                }

                for (int childe = 0; childe < nodesDic[i][j].children.Count; childe++)
                {
                    mapNodesData.childeIndex.Add(nodesDic[i][j].children[childe].index);
                }

                for (int d = 0; d < nodedatas.datas.Count; d++)
                {
                    if (nodedatas.datas[d] is CharactorData)
                    {
                        mapNodesData.monsterNames.Add(nodedatas.datas[d].name);
                    }
                    else if (nodedatas.datas[d] is EffectData)
                    {

                        mapNodesData.effectNames.Add(nodedatas.datas[d].name);
                    }
                }



                maptotalData.mapNodesDatas.Add(mapNodesData);
            }

        }

        File.WriteAllText(Application.dataPath + "/MapNodes.json", JsonUtility.ToJson(maptotalData));

    }


    public void ClearChange(int level, int index)
    {
        string pathData = File.ReadAllText(Application.dataPath + "/MapNodes.json");
        MaptotalData maptotalData = JsonUtility.FromJson<MaptotalData>(pathData);


        var findData = maptotalData.mapNodesDatas.Find(x => x.level == level && x.index == index);
        findData.clear = (int)Clear.Yes;

        File.WriteAllText(Application.dataPath + "/MapNodes.json", JsonUtility.ToJson(maptotalData));
    }


    public void DataLoadNode(object paramter)
    {
        if (!File.Exists(Application.dataPath + "/MapNodes.json"))
        {
            PlayerPrefs.SetInt("playersave", 1);
            PlayerPrefs.SetInt("playerlevel", 0);
            PlayerPrefs.SetInt("playerindex", 0);
            PlayerPrefs.SetInt("gold", 100000);

            EventManager.Emit("MakeScene", null);
            return;
        }

        string pathData = File.ReadAllText(Application.dataPath + "/MapNodes.json");


        MaptotalData maptotalData = JsonUtility.FromJson<MaptotalData>(pathData);



        for (int i = 0; i < maptotalData.mapNodesDatas.Count; i++)
        {
            TemporaryData temporaryData = new TemporaryData();
            Nodedatas nodedatas = new Nodedatas();
            StageNode<TemporaryData> stageNode = new StageNode<TemporaryData>();


            List<string> monsterNames = maptotalData.mapNodesDatas[i].monsterNames;
            List<string> effectNames = maptotalData.mapNodesDatas[i].effectNames;


            for (int strindex = 0; strindex < monsterNames.Count; strindex++)
            {
                string pathStr = string.Format("data/{0}", monsterNames[strindex]);
                CharactorData loadCharactor = Resources.Load<CharactorData>(pathStr);
                nodedatas.datas.Add(loadCharactor);

            }


            for (int strindex = 0; strindex < effectNames.Count; strindex++)
            {
                string pathStr = string.Format("EffectData/{0}", effectNames[strindex]);
                EffectData loadEffect = Resources.Load<EffectData>(pathStr);
                nodedatas.datas.Add(loadEffect);

            }

            nodedatas.state = (StageNodeState)maptotalData.mapNodesDatas[i].type;
            temporaryData.obj = nodedatas;

            stageNode.data = temporaryData;
            stageNode.level = maptotalData.mapNodesDatas[i].level;
            stageNode.index = maptotalData.mapNodesDatas[i].index;
            stageNode.clear = (Clear)maptotalData.mapNodesDatas[i].clear;
            if (!nodesDic.ContainsKey(stageNode.level))
            {
                nodesDic.Add(stageNode.level, new List<StageNode<TemporaryData>>());

            }

            nodesDic[stageNode.level].Add(stageNode);

            if (!nodesDic.ContainsKey(stageNode.level - 1))
            {
                continue;
            }

            List<int> parants = maptotalData.mapNodesDatas[i].parentIndex;
            for (int parent = 0; parent < parants.Count; parent++)
            {
                StageNode<TemporaryData> parantnode = nodesDic[stageNode.level - 1][parants[parent]];
                stageNode.parent.Add(parantnode);
                parantnode.children.Add(stageNode);
            }


        }



    }


    public void MakeinitStage(object parmater)
    {
        int index = 0;
        for (int i = 0; i < maxlevel; i++)
        {
            nodesDic.Add(i, new List<StageNode<TemporaryData>>());
        }

        TemporaryData data = new TemporaryData();
        data.obj = RandomCardSetting();


        StageNode<TemporaryData> stageNode = new StageNode<TemporaryData>();
        stageNode.data = data;
        stageNode.level = nodelevel;
        stageNode.index = index;
        stageNode.clear = Clear.No;
        AddNodesDicData(stageNode.level, stageNode);



        nodelevel++;

        for (int i = 1; i < nodesDic.Count; i++)
        {
            int randomCnt = Random.Range(2, maxNodeCnt + 1);
            index = 0;
            for (int r = 0; r < randomCnt; r++)
            {
                TemporaryData newdata = new TemporaryData();
                newdata.obj = RandomCardSetting();


                StageNode<TemporaryData> newstageNode = new StageNode<TemporaryData>();
                newstageNode.data = newdata;
                newstageNode.level = nodelevel;
                newstageNode.index = index;
                newstageNode.clear = Clear.No;
                AddNodesDicData(newstageNode.level, newstageNode);

                index++;
            }


            nodelevel++;

        }

        NodesLink();
    }



    private void NodesLink()
    {


        for (int i = 0; i < nodesDic.Count; i++)
        {
            if (!nodesDic.ContainsKey(i))
            {
                continue;
            }

            int next = i + 1;

            if (!nodesDic.ContainsKey(next))
            {
                continue;
            }


            if (nodesDic[i].Count == 1)
            {

                nodesDic[i][0].children.AddRange(nodesDic[next]);

                for (int childeInx = 0; childeInx < nodesDic[next].Count; childeInx++)
                {
                    nodesDic[next][childeInx].parent.Add(nodesDic[i][0]);
                }


            }
            else
            {
                for (int j = 0; j < nodesDic[i].Count; j++)
                {

                    if (nodesDic[next].Count > 0)
                    {

                        int random = Random.Range(0, nodesDic[next].Count);
                        nodesDic[i][j].children.Add(nodesDic[next][random]);
                        nodesDic[next][random].parent.Add(nodesDic[i][j]);
                    }
                }



            }




        }


        for (int i = 1; i < nodesDic.Count; i++)
        {
            if (!nodesDic.ContainsKey(i))
            {
                continue;
            }
            if (!nodesDic.ContainsKey(i - 1))
            {
                continue;
            }
            if (nodesDic[i - 1].Count == 0)
            {
                continue;
            }
            for (int j = 0; j < nodesDic[i].Count; j++)
            {
                if (nodesDic[i][j].parent.Count == 0)
                {

                    int randomIndex = Random.Range(0, nodesDic[i - 1].Count);
                    nodesDic[i][j].parent.Add(nodesDic[i - 1][randomIndex]);
                    nodesDic[i - 1][randomIndex].children.Add(nodesDic[i][j]);
                }

            }




        }

        DataSaveNode(null);
    }

    private Nodedatas RandomCardSetting()
    {
        Nodedatas nodedatas = new Nodedatas();
        nodedatas.RandomNodeData();
        return nodedatas;
    }








    private void AddNodesDicData(int level, StageNode<TemporaryData> stageNode)
    {
        if (nodesDic.ContainsKey(level))
        {
            nodesDic[level].Add(stageNode);
        }
    }


    private void PrintTree(StageNode<TemporaryData> stageNode)
    {



        foreach (var child in stageNode.children)
        {
            PrintTree(child);

        }

    }

    public void Show(object paramater)
    {
        PrintTree(root);
    }

    public int DeckCardsCnt()
    {

        if (!File.Exists(Application.dataPath + "/player.json"))
        {
            return 0;
        }


        string pathData = File.ReadAllText(Application.dataPath + "/player.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(pathData);

        return playerData.cardDackNames.Count;

    }


}
