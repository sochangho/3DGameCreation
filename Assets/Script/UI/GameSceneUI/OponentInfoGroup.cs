using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentInfoGroup : MonoBehaviour
{
    public OponentInfo infoUi;

    public void CreateInfoUi(string str)
    {
        OponentInfo go = Instantiate(infoUi);
        go.transform.parent = this.transform;
        go.infoText.text = str;
        
    }





}
