using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NodeButton : MonoBehaviour
{

    public List<SceneData> sceneDatas;
    private Button button;
    private Image image;
    private string sceneName;
    

    public void SceneInit(StageNodeState state)
    {
        foreach(SceneData data in sceneDatas)
        {
            if(data.state == state)
            {
                sceneName = data.scene;

                if (image == null)
                {
                    image = GetComponent<Image>();
                }
                image.sprite = data.icon;

            }
        }


        if(button == null)
        {
            button = GetComponent<Button>();
        }

        if(image == null)
        {
            image = GetComponent<Image>();
        }
        button.onClick.AddListener(OnClickNode);
    }

    private void OnClickNode()
    {

        SceneManager.LoadScene(sceneName);

    }


}

[System.Serializable]
public class SceneData
{
    public string scene;
    public Sprite icon;
    public StageNodeState state;
}