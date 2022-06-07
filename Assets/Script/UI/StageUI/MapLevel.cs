using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapLevel : MonoBehaviour
{
    public int level;
    public Button button;
    public Image lineImg;
    public Image dot;
    public MapArrow mapArrow;
    public GridLayoutGroup layoutGroup;


    private MapArrow cloneArrow;

    public void levelButtonSetting()
    {
        var nodesDic = StageManager.Instance.nodesDic;
        


        Vector2 levelmapsize = layoutGroup.cellSize;

        for (int level = 0; level < nodesDic.Count; level++)
        {
            var nodesList = nodesDic[level];
            float intervelX = levelmapsize.x / nodesList.Count;
            float intervelY = levelmapsize.y / nodesDic.Count;

            for (int i = 0; i < nodesList.Count; i++)
            {
                Vector2 pos = new Vector2(i * intervelX + intervelX / 2 - (levelmapsize.x / 2), level * intervelY + intervelY / 2 - (levelmapsize.y / 2));
                nodesDic[level][i].nodePos = pos;

                Button cloneBtn = null;

                if (!(level == 0 && i == 0) && nodesDic[level][i].clear == Clear.No)
                {
                    cloneBtn = Instantiate(button);
                    cloneBtn.transform.parent = this.transform;
                    cloneBtn.GetComponent<RectTransform>().position = pos;
                    Nodedatas nodedatas = (Nodedatas)nodesDic[level][i].data.obj;
                    cloneBtn.gameObject.GetComponent<NodeButton>().SceneInit(nodedatas.state);
                    cloneBtn.GetComponent<NodeButton>().nodeInfo = nodesDic[level][i];
                    cloneBtn.interactable = false;
                }
                else
                {
                    Image d = Instantiate(dot);
                    d.transform.parent = this.transform;
                    d.GetComponent<RectTransform>().position = pos;
                    Vector2 size = d.GetComponent<RectTransform>().sizeDelta;
                    d.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x/4 , size.y/4);
                }

                int curlevel = PlayerPrefs.GetInt("playerlevel");
                int curindex = PlayerPrefs.GetInt("playerindex");

                


              


                for(int parent = 0; parent < nodesDic[level][i].parent.Count; parent++)
                {
                    StageNode<TemporaryData> parentNode = nodesDic[level][i].parent[parent];
                    if (parentNode.level == curlevel && parentNode.index == curindex)
                    {

                        cloneBtn.interactable = true;
                        break;
                    }

                }




            }

        }



        Vector2 arrowPos = nodesDic[PlayerPrefs.GetInt("playerlevel")][PlayerPrefs.GetInt("playerindex")].nodePos;

        MapArrow arrow = Instantiate(mapArrow);
        arrow.transform.parent = this.transform;
        Vector2 sizeButton = button.GetComponent<RectTransform>().sizeDelta;
        arrow.GetComponent<RectTransform>().position = new Vector2(arrowPos.x, arrowPos.y + sizeButton.y);

        cloneArrow = arrow;



        LineImgLink();
    }

    public void LineImgLink()
    {

        var nodesDic = StageManager.Instance.nodesDic;


        for(int i = 0; i < nodesDic.Count; i++)
        {
            for(int j = 0; j < nodesDic[i].Count; j++)
            {
                

              

                for(int childe = 0; childe < nodesDic[i][j].children.Count; childe++)
                {

                    Vector2 pointStart = nodesDic[i][j].nodePos;
                    Vector2 pointEnd =  nodesDic[i][j].children[childe].nodePos;
                    Vector2 differenceVector = pointEnd - pointStart;
                    Vector2 clonePoint = (pointStart + pointEnd) / 2;
                    float distance = Vector2.Distance(pointStart, pointEnd);

                    var line  = Instantiate(lineImg);
                    line.transform.parent = this.transform;
                    line.GetComponent<RectTransform>().position = clonePoint;
                    line.GetComponent<RectTransform>().sizeDelta =  new Vector2(70 , distance * 7/10);
                    float angle = Mathf.Atan2(differenceVector.x, differenceVector.y) * Mathf.Rad2Deg;
                    line.transform.rotation = Quaternion.Euler(0, 0, -angle);

                    
                }



            }

        }


    }


    public void MoveArrow(Vector2 vector , System.Action action)
    {
        Vector2 sizeButton = button.GetComponent<RectTransform>().sizeDelta;
        Vector2 pos = new Vector2(vector.x, vector.y + sizeButton.y);
        StartCoroutine(GoArrow(pos , action));

    }

    IEnumerator GoArrow(Vector2 pos , System.Action action)
    {
        Vector2 arrowPos = cloneArrow.GetComponent<RectTransform>().position;
        Vector2 dir = (pos - arrowPos).normalized;

        while(Vector2.Distance(pos , arrowPos) > 1f)
        {
            cloneArrow.GetComponent<RectTransform>().Translate(dir * 30 * Time.deltaTime);

            arrowPos = cloneArrow.GetComponent<RectTransform>().position;

            yield return null;
        }


        if(action != null)
        {
            action();
        }


    }

}
