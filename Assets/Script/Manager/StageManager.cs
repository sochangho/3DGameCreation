using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : GameManager<StageManager>
{
    public Transform startTransform;
    public GameObject nodePrefab;
    public LineRenderer line;
    StageNode<TemporaryData> root;

   

    int maxlevel = 5;
    int index = 0;
    
   public void MakeinitStage()
   {
        TemporaryData dataTemp = new TemporaryData();
        dataTemp.obj = null;
        dataTemp.value = index;
        index++;

        root = new StageNode<TemporaryData>();
        root.data = dataTemp;

        var node  = Instantiate(nodePrefab);
        node.transform.position = startTransform.position;
        root.nodePos = node.transform.position;
        //루트 노드 초기화 

        maxlevel--;

        int randomCnt = Random.Range(2, 4);

        int interval = -1;

        for (int i = 0; i < randomCnt; i++)
        {
            TemporaryData dataChildTemp = new TemporaryData();
            dataChildTemp.obj = null;
            dataChildTemp.value = index;
            index++;
            StageNode<TemporaryData> childNode = new StageNode<TemporaryData>();
            childNode.data = dataChildTemp;

            var nodeChilde = Instantiate(nodePrefab);
           
            Vector3 nodeChildePos
                = new Vector3(root.nodePos.x + interval , root.nodePos.y + 2, root.nodePos.z);

            interval++;
            nodeChilde.transform.position = nodeChildePos;
            childNode.nodePos = nodeChilde.transform.position;

            var go  = Instantiate(line);

            go.SetPosition(0, root.nodePos);
            go.SetPosition(1, childNode.nodePos);

           

            root.children.Add(childNode);
            childNode.parent.Add(root);
            MakeStageNode(childNode);

           
        }

        

    }
   
    private void MakeStageNode(StageNode<TemporaryData> stageNode)
    {
        if(maxlevel == 0)
        {
            return;
        }
        maxlevel--;
        int randomCnt = Random.Range(1, 4);
        int interval = 0;
        for (int i = 0; i < randomCnt; i++)
        {
            TemporaryData dataChildTemp = new TemporaryData();
            dataChildTemp.obj = null;
            dataChildTemp.value = index;
            index++;
            StageNode<TemporaryData> childNode = new StageNode<TemporaryData>();
            childNode.data = dataChildTemp;

            //노드 생성 ----------------------------------------------------------------------------------
            var nodeChilde = Instantiate(nodePrefab);
            
            Vector3 nodeChildePos
                = new Vector3(root.nodePos.x + interval * Mathf.Abs(stageNode.nodePos.y - root.nodePos.y)/randomCnt, stageNode.nodePos.y + 4, stageNode.nodePos.z);
            interval++;

            nodeChilde.transform.position = nodeChildePos;
            childNode.nodePos = nodeChilde.transform.position;

            var go = Instantiate(line);

            go.SetPosition(0, stageNode.nodePos);
            go.SetPosition(1, childNode.nodePos);

            
            //---------------------------------------------------------------------------------------------
            stageNode.children.Add(childNode);
            childNode.parent.Add(stageNode);
            MakeStageNode(childNode);

        }

       
    }


    public void FindZerochildLink()
    {

        List<StageNode<TemporaryData>> stageNodes = new List<StageNode<TemporaryData>>();

        DetectTreeZero(stageNodes, root);

        
        foreach(var stageNode in stageNodes)
        {
            if(stageNode.parent.Count == 0)
            {
                continue;
            }

            var next = stageNode.parent[0].children[0].children;
            List<StageNode<TemporaryData>> tempStageNodes = new List<StageNode<TemporaryData>>();
            foreach(var n in next)
            {
                tempStageNodes.Add(n);

            }
            
            if(tempStageNodes.Count == 0)
            {
                continue;
            }

            int randomIndex = Random.Range(0, tempStageNodes.Count);

            StageNode<TemporaryData> childe = tempStageNodes[randomIndex];

            stageNode.children.Add(childe);
            childe.parent.Add(stageNode);


            var go = Instantiate(line);

            go.SetPosition(0, stageNode.nodePos);
            go.SetPosition(1, childe.nodePos);

        }


    }


    private void DetectTreeZero(List<StageNode<TemporaryData>> stageNodes, StageNode<TemporaryData> stageNode)
    {
        if(stageNode.children.Count == 0)
        {
            stageNodes.Add(stageNode);

        }

        foreach (var child in stageNode.children)
        {
            DetectTreeZero(stageNodes, child);

        }



    }


    private void PrintTree(StageNode<TemporaryData> stageNode)
    {
        Debug.Log(stageNode.data.value);


        foreach(var child in stageNode.children)
        {
            PrintTree(child);

        }

    }

    public void Show()
    {
        PrintTree(root);
    }
}
