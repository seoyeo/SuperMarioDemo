using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopColliderForTortoise : MonoBehaviour {

    public delegate void TortoiseSleepHander();
    public event TortoiseSleepHander TortoiseSleepEvent;

    TortoiseControl tc;
	// Use this for initialization
	void Start () {
        tc = GetComponentInParent<TortoiseControl>();
        TortoiseSleepEvent += tc.Sleep;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (TortoiseSleepEvent != null)
                TortoiseSleepEvent();
        }
    }
}
