using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    
    public TextMeshProUGUI curTimeTex;
    
    public TextMeshProUGUI maxTimeTex;
    
    public Image timeFillImg;

    public Animator frameAnim;


    public void TimerStart()
    {
        frameAnim.enabled = true;
    }
    public void TimerEnd()
    {
        frameAnim.enabled = false;
    }

    public void TimerSet(float curtime , float maxtime )
    {
        curTimeTex.text = ChangeTexTime(curtime);
        maxTimeTex.text = ChangeTexTime(maxtime);

        timeFillImg.fillAmount = (maxtime - curtime) / maxtime;

    }

    public string ChangeTexTime(float time)
    {
        string timeStr = null;

        int minitue = (int)(time/60);
        int second = (int)(time % 60);
        string minuteStr = minitue.ToString();
        string secondStr = null; 
        if(second < 10)
        {

            secondStr = string.Format("{0}{1}", 0, second);

        }
        else
        {
            secondStr = string.Format("{0}",second);
        }

        timeStr = string.Format("{0}:{1}", minuteStr, secondStr);
        
        return timeStr;
    }



}
