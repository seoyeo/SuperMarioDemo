using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{

    BoxCollider2D bc;
    int dethID = Animator.StringToHash("deth");

    // Use this for initialization
    void Start()
    {
        isFaceLeft = true;
        speed = 300;
        state = EnemnState.run;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        isSleep = false;
    }

    void Update()
    {
        if (state == EnemnState.die)
        {
            OnDestroyTheGame();
        }
        if (state != EnemnState.die)
        {
            ChenckObstacle();
            Run();
        }
    }

    private void OnDestroyTheGame()
    {
        bc.enabled = false;
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > 1f && info.IsName("deth"))
            Destroy(this.gameObject);
    }

    public void Deth()
    {
        state = EnemnState.die;
        anim.SetBool(dethID, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "TortoiseEnemy")
            Deth();
    }
}
