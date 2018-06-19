using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBrick : MonoBehaviour {

    public bool isTrriger;
    bool isEnable;
    int openId = Animator.StringToHash("open");
    [SerializeField]
    GameObject mushroom;

    Animator anim;
	// Use this for initialization
	void Start () {
        isTrriger = false;
        isEnable = true;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player"&&isEnable)
        {
            isTrriger = true;
            isEnable = false;
            anim.SetBool(openId, true);
            mushroom.SetActive(true);
        }
    }
}
