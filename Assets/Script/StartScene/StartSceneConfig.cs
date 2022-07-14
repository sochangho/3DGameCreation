using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSceneConfig : MonoBehaviour
{

    public Button startButton;
    public Button ExitButton;
    public CharacterMotion characterMotion;

    public void Start()
    {
        startButton.onClick.AddListener( characterMotion.StartGameMotion);
        ExitButton.onClick.AddListener(OnClickExit);
    }

    public void OnClickExit()
    {
        GameSystemManager.Instance.GameExit();
    }




}
