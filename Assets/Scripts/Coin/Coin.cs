using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{

    delegate void SetScoreHander(int score);
    event SetScoreHander SetScoreEvent;

    UIManager UImanager;
    Animator anima;
    // Use this for initialization
    void Start()
    {
        anima = GetComponent<Animator>();
        UImanager = FindObjectOfType<UIManager>();
        SetScoreEvent = UImanager.SetScoreNumber;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = anima.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > 1 && info.IsName("open"))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anima.SetBool("open", true);
            AudioManager.audioManager.PlayerVoice("Coin");
            transform.DOMoveY(transform.position.y + 0.8f, 0.8f, false);
            if (SetScoreEvent != null)
                SetScoreEvent(100);
        }
    }
}
