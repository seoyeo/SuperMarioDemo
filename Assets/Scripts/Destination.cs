using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{

    delegate void DestinationHander();
    event DestinationHander DestinationEvent;

    UIManager UImanager;

    // Use this for initialization
    void Start()
    {
        UImanager = FindObjectOfType<UIManager>();
        DestinationEvent += UImanager.Destination;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //        if (DestinationEvent != null) DestinationEvent();
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            if (DestinationEvent != null) DestinationEvent();
    }
}
