using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : GameManager<StageManager>
{

    StageNode<TemporaryData> root;

    readonly public Dictionary<int, List<StageNode<TemporaryData>>> nodesDic = new Dictionary<int, List<StageNode<TemporaryData>>>();

    public int nodelevel { get; set; } = 0;
   

    int maxlevel = 7;
    int maxNodeCnt = 3;


    public void MakeinitStage(object parmater)
   {
        int index = 0;
        for (int i = 0; i < maxlevel; i++)
        {
            nodesDic.Add(i, new List<StageNode<TemporaryData>>());
        }

        TemporaryData data = new TemporaryData();
        data.obj = null;
     

        StageNode<TemporaryData> stageNode = new StageNode<TemporaryData>();
        stageNode.data = data;
        stageNode.level = nodelevel;
        stageNode.index = index;
        AddNodesDicData(stageNode.level, stageNode);
        
        
        
        nodelevel++;

        for(int i = 1; i < nodesDic.Count; i++)
        {
            int randomCnt = Random.Range(1, maxNodeCnt + 1);
            index = 0;
            for(int r = 0; r < randomCnt; r++)
            {
                TemporaryData newdata = new TemporaryData();
                newdata.obj = null;
                

                StageNode<TemporaryData> newstageNode = new StageNode<TemporaryData>();
                newstageNode.data = newdata;
                newstageNode.level = nodelevel;
                newstageNode.index = index;
                AddNodesDicData(newstageNode.level, newstageNode);

                index++;
            }

            
            nodelevel++;

        }

        NodesLink();
    }
    


    private void NodesLink()
    {

        
        for(int i = 0; i < nodesDic.Count; i++)
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


            if(nodesDic[i].Count == 1)
            {

                nodesDic[i][0].children.AddRange(nodesDic[next]);

                for(int childeInx = 0; childeInx < nodesDic[next].Count; childeInx++)
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



        







    }





    

    private void AddNodesDicData(int level , StageNode<TemporaryData> stageNode)
    {
        if (nodesDic.ContainsKey(level))
        {
            nodesDic[level].Add(stageNode);
        }
    }


    private void PrintTree(StageNode<TemporaryData> stageNode)
    {
       


        foreach(var child in stageNode.children)
        {
            PrintTree(child);

        }

    }

    public void Show(object paramater)
    {
        PrintTree(root);
    }
}
