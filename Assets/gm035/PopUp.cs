using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public struct PopupOption
{
    public GameObject obj;
    public int stageNum;
    public int enemyCount;
    public int startGold;
}

public class PopUp : MonoBehaviour
{
    public PopupOption stage;
    public TMP_Text stageText;
    public TMP_Text enemyText;
    public TMP_Text startGoldText;
    public Button[] btnAry;

    private void Start()
    {
        stageText.text = "Stage : "+ stage.stageNum.ToString();
        enemyText.text = "Enemy Count : " + stage.enemyCount.ToString();
        startGoldText.text = "Start Gold : " + stage.startGold.ToString();
    }
    private void Update()
    {
        bool popupIsActive = stage.obj != null && stage.obj.activeInHierarchy;

        foreach (Button button in btnAry)
        {
            if (button != null)
                button.interactable = !popupIsActive;
        }
    }
}
