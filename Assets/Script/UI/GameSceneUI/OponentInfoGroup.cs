using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentInfoGroup : MonoBehaviour
{
    public InfoCreater infoUi;

    public void CreateInfoUi(string str)
    {
        InfoCreater go = Instantiate(infoUi);
        go.transform.parent = this.transform;
        go.infoText.text = str;
        
    }





}
