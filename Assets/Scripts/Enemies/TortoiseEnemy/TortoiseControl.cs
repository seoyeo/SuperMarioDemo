using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortoiseControl : Enemy
{

    int sleepID = Animator.StringToHash("sleep");

    float sleepTime;
    //踩踏的次数
    bool treadFrequency;

    void Start()
    {
        isFaceLeft = true;
        speed = 300;
        sleepTime = 0;
        treadFrequency = false;
        isSleep = false;
        state = EnemnState.run;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (state == EnemnState.sleep)
        {
            anim.SetBool(sleepID, true);
            speed = 900;
            if (Time.time > sleepTime)
            {
                state = EnemnState.run;
                anim.SetBool(sleepID, false);
                speed = 300;
                sleepTime = 0;
                treadFrequency = false;
            }
        }

        if (!treadFrequency)
        {
            Run();
            ChenckObstacle();
        }

    }

    public void Sleep()
    {
        state = EnemnState.sleep;
        sleepTime = Time.time + 10f;
        treadFrequency = !treadFrequency;
        isSleep = true;
    }
}
