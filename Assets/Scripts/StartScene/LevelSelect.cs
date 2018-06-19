using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    [SerializeField]
    GameObject LockCard;
    [SerializeField]
    Text LevelNumber;
    [SerializeField]
    Image StarsPanel;

    [SerializeField]
    Sprite[] stars;

    bool isLock;
    //当前所在关卡数字
    int levelNumber;
    //判断是否可以通关
    int classNumber;

    delegate void LoadTipHandler();
    event LoadTipHandler LoadTipEvent;

    StartSceneManager ss;
    // Use this for initialization
    void Start()
    {
        ss = FindObjectOfType<StartSceneManager>();
        LoadTipEvent += ss.LoadTip;
        int.TryParse(LevelNumber.text, out levelNumber);
        UnlockCard();
        ShowStars();
    }

    // Update is called once per frame
    void Update()
    {
        if (LockCard.activeInHierarchy)
            isLock = true;
        else if (!LockCard.activeInHierarchy)
            isLock = false;
    }

    public void SelectLevel()
    {
        if (!isLock)
        {
            if (LoadTipEvent != null)
                LoadTipEvent();
            StartCoroutine(LoadScene());
        }
        else if (!isLock)
            Debug.Log("还未解锁");
    }

    public void UnlockCard()
    {
        switch (levelNumber)
        {
            case 1:
                LockCard.SetActive(false);

                break;
            case 2:
                classNumber = PlayerPrefs.GetInt("1");
                if (classNumber > 0)
                    LockCard.SetActive(false);
                break;
            case 3:
                classNumber = PlayerPrefs.GetInt("2");
                if (classNumber > 0)
                    LockCard.SetActive(false);
                break;
            case 4:
                classNumber = PlayerPrefs.GetInt("3");
                if (classNumber > 0)
                    LockCard.SetActive(false);
                break;
            case 5:
                classNumber = PlayerPrefs.GetInt("4");
                if (classNumber > 0)
                    LockCard.SetActive(false);
                break;
            case 6:
                classNumber = PlayerPrefs.GetInt("5");
                if (classNumber > 0)
                    LockCard.SetActive(false);
                break;
            default:
                break;
        }
    }

    void ShowStars()
    {
        switch (classNumber)
        {
            case 1:
                StarsPanel.sprite = stars[0];
                break;
            case 2:
                StarsPanel.sprite = stars[1];
                break;
            case 3:
                StarsPanel.sprite = stars[2];
                break;
            default:
                break;
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelNumber);
    }
}
