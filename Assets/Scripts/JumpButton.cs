using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    public delegate void JumpButtonDownHandler();
    public event JumpButtonDownHandler JumpButtonEvent;

    MarioControl mc;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (JumpButtonEvent != null)
        {
            JumpButtonEvent();
        }
    }
    private void Start()
    {
        mc = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioControl>();
        JumpButtonEvent += mc.MobileJump;
    }
    public void FindPlayer()
    {
        mc = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioControl>();
        JumpButtonEvent += mc.MobileJump;
    }
}
