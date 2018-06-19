using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{

    [SerializeField]
    GameObject Play_Btn;
    [SerializeField]
    GameObject More_Btn;
    [SerializeField]
    GameObject Exit_Btn;

    [SerializeField]
    GameObject StartPanel;
    [SerializeField]
    GameObject SelectPanel;
    [SerializeField]
    GameObject MorePanel;
    [SerializeField]
    GameObject LoadTipPanel;

    // Use this for initialization
    void Start()
    {
        Play_Btn.transform.DOMoveY(0.8f, 1f, false);
        More_Btn.transform.DOMoveX(0, 1f, false);
        Exit_Btn.transform.DOMoveX(Exit_Btn.transform.position.x - 3f, 1, false);
    }

    public void PlayButtonClick()
    {
        StartPanel.transform.DOMoveX(-15, 1f, false);
        SelectPanel.transform.DOMoveX(0, 1f, false);
    }

    /// <summary>
    /// 更多Button
    /// </summary>
    public void MoreButtonClick()
    {
        MorePanel.transform.DOMoveY(0, 1, false);
        StartPanel.transform.DOMoveX(-15, 1f, false);
    }

    /// <summary>
    /// 回到主页
    /// </summary>
    public void MoreToStartButton()
    {
        MorePanel.transform.DOMoveY(-10, 1, false);
        StartPanel.transform.DOMoveX(0, 1f, false);
    }

    /// <summary>
    /// 回到主页
    /// </summary>
    public void LevelSelectToStartButton()
    {
        StartPanel.transform.DOMoveX(0, 1f, false);
        SelectPanel.transform.DOMoveX(-10, 1f, false);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void LoadTip()
    {
        LoadTipPanel.transform.DOMoveX(0, 1, false);
        SelectPanel.transform.DOMoveX(-10, 1f, false);
    }
}
