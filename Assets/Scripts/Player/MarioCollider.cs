using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioCollider : MonoBehaviour
{

    delegate void DamageHander();
    delegate void DethHander();
    delegate void ChangeBigHander();
    delegate void BoundaryDethHander();
    delegate void SetKillHander();

    event DamageHander DamageEvent;
    event DethHander DethEvent;
    event ChangeBigHander ChangeBigEvent;
    event BoundaryDethHander BoundaryDethEvent;
    event SetKillHander SetKillEvent;

    MarioControl mc;
    UIManager UImanager;

    // Use this for initialization
    void Start()
    {
        mc = GetComponent<MarioControl>();
        UImanager = FindObjectOfType<UIManager>();
        SetKillEvent += UImanager.SetKillNumber;
        DamageEvent += mc.SmallJump;
        DethEvent += mc.CollisionEnemy;
        ChangeBigEvent += mc.ChangeBig;
        BoundaryDethEvent += mc.SetDeth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "KillTortoise")
            if (DamageEvent != null)
            {
                AudioManager.audioManager.PlayerVoice("kill");
                DamageEvent();
            }
        if (collision.collider.tag == "KillMushroomEnemy")
        {
            if (DamageEvent != null) DamageEvent();
            if (SetKillEvent != null) SetKillEvent();
            AudioManager.audioManager.PlayerVoice("kill");
        }

        if (collision.collider.tag == "MushroomEnemy" || collision.collider.tag == "TortoiseEnemy")
            if (DethEvent != null)
            {
                DethEvent();
            }
        if (collision.collider.tag == "Boundary")
            if (BoundaryDethEvent != null)
            {
                BoundaryDethEvent();
                AudioManager.audioManager.PlayerVoice("Deth");
            }
        if (collision.collider.tag == "Mushroom")
        {
            AudioManager.audioManager.PlayerVoice("EatMushroom");
            if (ChangeBigEvent != null)
                ChangeBigEvent();
            Destroy(collision.gameObject);
        }
    }
}
