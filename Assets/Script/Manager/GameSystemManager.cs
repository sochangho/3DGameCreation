using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameSystemManager : GameManager<GameSystemManager>
{
    
    public void GameExit(Action exitAction = null )
    {

        if(exitAction != null)
        {
            exitAction();
        }


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif

    }



}
