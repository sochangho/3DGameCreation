using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameStateUi : MonoBehaviour
{
    [SerializeField]
    private GameObject readyUi;

    [SerializeField]
    private GameObject startUi;

    [SerializeField]
    private GameObject winUi;

    [SerializeField]
    private GameObject gameOverUi;

    public Action gameEndAction;

    public void GameStart()
    {
        StartCoroutine(GameStartUiRotin());

    }

    public void Win(Action action)
    {
        gameEndAction = action;
        StartCoroutine(WinUiRotin());

    }
    public void GameOver(Action action)
    {
        gameEndAction = action;
        StartCoroutine(GameOverUiRotin());

    }

    IEnumerator GameStartUiRotin()
    {
        float time = 0;

        readyUi.SetActive(true);

        while(time < 2f)
        {

            if(time > 1f)
            {
                if (readyUi.activeSelf)
                {
                    readyUi.SetActive(false);
                }

                if (!startUi.activeSelf)
                {
                    startUi.SetActive(true);
                }
                
            }

            time += Time.deltaTime;

            yield return null;
        }

        startUi.SetActive(false);

        GameSceneManager.Instance.GameStart();

    }

    IEnumerator WinUiRotin()
    {
        float time = 0;
        winUi.SetActive(true);
        while (time < 1f)
        {

            time += Time.deltaTime;
            yield return null;
        }

        winUi.SetActive(false);
        if(gameEndAction != null)
        {
            gameEndAction();
        }
    }

    IEnumerator GameOverUiRotin()
    {

        float time = 0;
        gameOverUi.SetActive(true);
        while (time < 1f)
        {

            time += Time.deltaTime;
            yield return null;
        }
        gameOverUi.SetActive(false);

        if (gameEndAction != null)
        {
            gameEndAction();
        }
    }

}
