using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarsControl : MonoBehaviour
{

    bool isOpen;
    // Use this for initialization
    void Start()
    {
        Debug.Log("so===");
        isOpen = false;
        transform.localScale = new Vector3(8, 8, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            transform.DOScale(1f, 0.6f);
            //transform.DOMoveX(1, 1f, false);
            isOpen = true;
        }
    }
}
