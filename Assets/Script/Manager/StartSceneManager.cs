using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartSceneManager : GameManager<StartSceneManager>
{
   
    public void GameStart()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void GameExit()
    {


    }


}
