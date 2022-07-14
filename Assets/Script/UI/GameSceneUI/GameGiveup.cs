using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameGiveup : MonoBehaviour
{
    [SerializeField]
    public Button exitbutton;
    [SerializeField]
    public Button cancelButton;

    private void Start()
    {

        exitbutton.onClick.AddListener(GameGiveupExit);
        exitbutton.onClick.AddListener(Cancel);

        cancelButton.onClick.AddListener(Cancel);


    }

    private void GameGiveupExit()
    {
        if (!GameSceneManager.Instance.is_gameEnd)
        {
            GameSceneManager.Instance.is_gameEnd = true;

            ParameterHelper parameterHelper = new ParameterHelper();
            Player ownplayer = GameSceneManager.Instance.ownPlayer;
                      
            parameterHelper.objList.Add(ownplayer);
            parameterHelper.objList.Add(ownplayer.tower);

            
            EventManager.Emit("GameEnd", parameterHelper);


        }
    }

    private void Cancel()
    {

        Destroy(this.gameObject);
    }


}
