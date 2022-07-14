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
        timer.TimerSet(curtime, maxtime);

        while (curtime < maxtime)
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

            var ownObj = ownplayer.GetObjects<AimObject>();
            var oponentObj = oponentplayer.GetObjects<AimObject>();

            Tower ownTower = null;
            Tower oponentTower = null;

            for (int i = 0; i < ownObj.Count; i++)
            {
                if (ownObj[i] is Tower)
                {

                    ownTower = ownObj[i] as Tower;
                    break;
                }

            }

            for (int i = 0; i < oponentObj.Count; i++)
            {

                if (oponentObj[i] is Tower)
                {

                    oponentTower = oponentObj[i] as Tower;
                    break;
                }

            }

            if (ownTower == null)
            {

                Debug.LogError("타워 x");
            }
            if (oponentTower == null)
            {

                Debug.LogError("타워 x");
            }

            float ownHp = ownTower.cur_hp / ownTower.hp;
            float oponentHp = oponentTower.cur_hp / oponentTower.hp;




            if (ownHp > oponentHp)
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
