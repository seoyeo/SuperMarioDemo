using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    delegate void SetLifeHander(int life);
    delegate void NewPlayerHandler();

    event SetLifeHander SetLifeEvent;
    event NewPlayerHandler NewPlayerEvent;

    UIManager UImanager;
    MobileControl mobile;
    JumpButton jb;

    int lifeNumber;

    [SerializeField]
    Transform relifePostion1;
    [SerializeField]
    Transform relifePostion2;

    GameObject marioPrefab;

    // Use this for initialization
    void Start()
    {
        lifeNumber = 3;
        UImanager = FindObjectOfType<UIManager>();
        mobile = FindObjectOfType<MobileControl>();
        jb = FindObjectOfType<JumpButton>();

        SetLifeEvent += UImanager.SetLifeNumber;
        NewPlayerEvent += mobile.FindPlayer;
        NewPlayerEvent += jb.FindPlayer;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReLife(Vector3 pos)
    {
        if (lifeNumber > 1)
        {
            StartCoroutine(InstantiateMario(pos));
            lifeNumber--;
            if (SetLifeEvent != null)
                SetLifeEvent(lifeNumber);
        }
        else
        {
            UImanager.OpenDethPanel();
        }
    }

    IEnumerator InstantiateMario(Vector3 pos)
    {
        yield return new WaitForSeconds(2);
        marioPrefab = Instantiate(Resources.Load("SuperMario", typeof(GameObject)) as GameObject);
        if (relifePostion2.position.x < pos.x)
            marioPrefab.transform.position = relifePostion2.position;
        else
            marioPrefab.transform.position = relifePostion1.position;
        if (NewPlayerEvent != null)
            NewPlayerEvent();
    }
}
