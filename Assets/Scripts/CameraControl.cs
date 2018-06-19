using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            Debug.Log("Not Find Player");
        }

    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 currenrPostion = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = currenrPostion;
        }
    }
}
