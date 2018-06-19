using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    delegate void JunpHandler();

    event JunpHandler JumpEvent;

    MarioControl mc;
    GameManager gm;

    [SerializeField]
    Text Score;
    [SerializeField]
    Text Life;
    [SerializeField]
    Text KillNumber;

    [SerializeField]
    GameObject PausedPanel;
    [SerializeField]
    GameObject Head;
    [SerializeField]
    GameObject PausedPanel_Btn;
    [SerializeField]
    GameObject LevelCleared;
    [SerializeField]
    GameObject DethPanel;
    [SerializeField]
    GameObject MobileControlPanel;

    [SerializeField]
    GameObject[] Stars;
    [SerializeField]
    Text FinalScoreText;
    [SerializeField]
    Text EvaluateText;

    float scoreNumber;
    float lifeNumber;
    int killNumber;

    bool pausedPanelOpen;
    private int finalScore;
    private bool isEnd;

    // Use this for initialization
    void Start()
    {
        scoreNumber = 0;
        lifeNumber = 3;
        killNumber = 0;
        isEnd = false;
        pausedPanelOpen = false;
        Head.transform.DOMoveY(Head.transform.position.y - 1.2f, 1f, false);
        mc = FindObjectOfType<MarioControl>();
        JumpEvent += mc.MobileJump;
    }

   
    // Update is called once per frame
    void Update()
    {
        RefreshHead();
        if (isEnd)
        {
            StartCoroutine(FinalScoreShow());
        }
    }

    IEnumerator FinalScoreShow()
    {
        for (int i = 0; i < finalScore + 1; i++)
        {
            FinalScoreText.text = "ScoreX" + i.ToString();
            i = i + 9;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void RefreshHead()
    {
        Score.text = "X" + scoreNumber;
        Life.text = "X" + lifeNumber;
        KillNumber.text = "X" + killNumber;
    }
    /// <summary>
    /// 设置顶部面板分数
    /// </summary>
    /// <param name="score"></param>
    public void SetScoreNumber(int score)
    {
        scoreNumber = scoreNumber + score;
    }

    /// <summary>
    /// 显示声明数量
    /// </summary>
    /// <param name="life"></param>
    public void SetLifeNumber(int life)
    {
        lifeNumber = life;
    }

    /// <summary>
    /// 显示杀敌数量
    /// </summary>
    public void SetKillNumber()
    {
        killNumber++;
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void OpenPausedPanel()
    {
        if (pausedPanelOpen)
        {
            PausedPanel.SetActive(false);
            PausedPanel_Btn.SetActive(true);
            MobileControlPanel.SetActive(true);
            pausedPanelOpen = !pausedPanelOpen;
            Time.timeScale = 1;
        }
        else if (!pausedPanelOpen)
        {
            PausedPanel_Btn.SetActive(false);
            PausedPanel.SetActive(true);
            MobileControlPanel.SetActive(false);
            pausedPanelOpen = !pausedPanelOpen;
            Time.timeScale = 0;
        }
    }
    /// <summary>
    /// 到达终点
    /// </summary>
    public void Destination()
    {
        LevelCleared.SetActive(true);
        MobileControlPanel.SetActive(false);
        PausedPanel_Btn.SetActive(false);
        AudioManager.audioManager.PlayerVoice("Win");
        Settlement();
    }
    /// <summary>
    /// 结算
    /// </summary>
    public void Settlement()
    {
        finalScore = (int)(scoreNumber + killNumber * 200);
        int index = 0;
        if (finalScore > 800)
        {
            EvaluateText.text = "你很强";
            index = 3;
        }
        else if (400 <= finalScore && finalScore < 800)
        {
            index = 2;
            EvaluateText.text = "干点漂亮";
        }
        else if (200 <= finalScore && finalScore < 400)
        {
            EvaluateText.text = "勉勉强强";
            index = 1;
        }
        else
        {
            index = 0;
            EvaluateText.text = "太菜了";
        }
        PlayerPrefs.SetInt(SceneManager.sceneCount.ToString(), index);
        isEnd = true;
        for (int i = 0; i < index; i++)
        {
            if (index > 0)
            {
                Stars[i].SetActive(true);
            }
        }
    }
    /// <summary>
    /// 返回首页
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// 重玩
    /// </summary>
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    ///打开死亡面板
    /// </summary>
    public void OpenDethPanel()
    {
        DethPanel.SetActive(true);
        PausedPanel_Btn.SetActive(false);
        AudioManager.audioManager.PlayerVoice("Gameover");
    }
    
    public void LoadSceneTow()
    {
        SceneManager.LoadScene(2);
    }
}
