using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.IO;
public class StageSceneConfig : MonoBehaviour
{
    public Inventory inventory;
    public Canvas canvas;
    public Canvas infoCanvas;

    public Button cardInventoryButton;
    public Button backButton;
    public Button exitButton;

    public List<CharactorData> datas;
    public List<EffectData> effectdatas;
    public List<TowerData> towerdatas;

    public EndGame gameExit;

    private void Awake()
    {

        EventManager.On("MakeScene", StageManager.Instance.MakeinitStage);
        EventManager.On("MakeScene", PlayerDataSetInit);
        EventManager.On("LoadScene", StageManager.Instance.DataLoadNode);


        if (!PlayerPrefs.HasKey("playersave") || PlayerPrefs.GetInt("playersave") == 0)
        {
            PlayerPrefs.SetInt("playersave", 1);
            PlayerPrefs.SetInt("playerlevel", 0);
            PlayerPrefs.SetInt("playerindex", 0);
            PlayerPrefs.SetInt("gold", 100000);

            EventManager.Emit("MakeScene", null);

        }
        else
        {

            EventManager.Emit("LoadScene", null);

        }


        cardInventoryButton.onClick.AddListener(InventoryButtonClick);
        cardInventoryButton.onClick.AddListener(ButtonUiActive);

        backButton.onClick.AddListener(OnClickBack);
        exitButton.onClick.AddListener(OnClickCreateGameExit);

    }





    private void PlayerDataSetInit(object parameter)
    {
        PlayerPrefs.SetInt("playersave", 1);

        PlayerData playerData = new PlayerData();


        foreach (CharactorData data in datas)
        {

            playerData.cardNames.Add(data.name);

        }

        foreach (EffectData data in effectdatas)
        {
            playerData.effectNams.Add(data.name);
        }

        foreach (TowerData towerData in towerdatas)
        {

            playerData.towerNames.Add(towerData.name);
        }


        playerData.towerName = playerData.towerNames[0];

        File.WriteAllText(Application.dataPath + "/player.json", JsonUtility.ToJson(playerData));
    }

    public void InventoryButtonClick()
    {

        Inventory go = Instantiate(inventory);
        go.transform.parent = canvas.transform;
        go.GetComponent<RectTransform>().localPosition = new Vector2(-6.654907f, -11f);

    }
    public void TowerInvButtonClick()
    {

    }

    public void ButtonUiActive()
    {
        if (cardInventoryButton.interactable)
        {
            cardInventoryButton.interactable = false;
        }
        else
        {
            cardInventoryButton.interactable = true;
        }


    }

    private void OnClickCreateGameExit()
    {
        EndGame endGame = Instantiate(gameExit);
        endGame.transform.parent = canvas.transform;
        endGame.GetComponent<RectTransform>().localPosition = Vector2.zero;
        endGame.GetComponent<RectTransform>().localScale = Vector2.one;

        endGame.EndGameSet("������ �����ұ��??", Exit);

    }

    private void Exit()
    {
        GameSystemManager.Instance.GameExit();
    }

    private void OnClickBack()
    {
        SceneManager.LoadScene("StartScene");
    }
}
