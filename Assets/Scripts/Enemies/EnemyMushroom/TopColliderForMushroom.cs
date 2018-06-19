using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopColliderForMushroom : MonoBehaviour {
    public EnemyMushroom em;

    public delegate void EnemyDethHander();
    public event EnemyDethHander EnemyDethEven;

	// Use this for initialization
	void Start () {
        em = GetComponentInParent<EnemyMushroom>();
        EnemyDethEven += em.Deth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (EnemyDethEven != null)
                EnemyDethEven();
        }
    }
}
