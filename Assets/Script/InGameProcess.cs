using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameProcess : MonoBehaviour
{


    [SerializeField]
    private Timer timer;

    public float curtime = 0;
    public float maxtime;


    public void Awake()
    {         
        EventManager.On("GameStart", GameTimerStart);
    }

    public void GameTimerStart(object obj)
    {
        StartCoroutine(GameTimerRouitin());
        
    }


    IEnumerator GameTimerRouitin()
    {
        timer.TimerStart();
        timer.TimerSet(curtime , maxtime);

        while(curtime < maxtime)
        {
            curtime += Time.deltaTime;
            timer.TimerSet(curtime, maxtime);
            yield return null;
        }

        curtime = maxtime;
        timer.TimerEnd();



        if (!GameSceneManager.Instance.is_gameEnd)
        {
            GameSceneManager.Instance.is_gameEnd = true;

            ParameterHelper parameterHelper = new ParameterHelper();
            Player ownplayer = GameSceneManager.Instance.ownPlayer;
            Player oponentplayer = GameSceneManager.Instance.oponentPlayer;

            if (ownplayer.tower.hp > oponentplayer.tower.hp)
            {
                parameterHelper.objList.Add(oponentplayer);
                parameterHelper.objList.Add(oponentplayer.tower);

            }
            else
            {
                parameterHelper.objList.Add(ownplayer);
                parameterHelper.objList.Add(ownplayer.tower);

            }
            EventManager.Emit("GameEnd", parameterHelper);


        }
       

        
       
    }


}
