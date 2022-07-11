using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class EndGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI infoText;
    [SerializeField]
    private Button exitButton;


    public void EndGameSet(string infoStr ,UnityAction unityAction)
    {
        infoText.text = infoStr;
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(unityAction);
    }



}
