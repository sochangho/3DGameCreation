using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapLevel : MonoBehaviour
{
    public int level;
    public Button button;
    public GridLayoutGroup layoutGroup;

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
                Button cloneBtn = Instantiate(button);
                cloneBtn.transform.parent = this.transform;
                cloneBtn.GetComponent<RectTransform>().position = pos;

            }

        }

    }

}
