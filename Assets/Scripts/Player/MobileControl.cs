using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileControl : ScrollRect
{
    //把拖动信息传出去
    delegate void OnDragHandler(Vector2 drag);
    event OnDragHandler OnDragEvent;

    MarioControl mc;

    // 半径
    private float dragRdius = 0f;

    // 距离
    private const float Dis = 0.5f;

    private Vector2 v2;

    //摇杆的位置
    private Vector2 contentPosition;

    protected override void Start()
    {
        base.Start();
        mc = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioControl>();
        OnDragEvent += mc.SetDrag;

        // 能移动的半径 = 摇杆的宽 * Dis
        dragRdius = content.sizeDelta.x * Dis;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // 获取摇杆，根据锚点的位置。
        contentPosition = content.anchoredPosition;

        // 判断摇杆的位置是否大于半径
        if (contentPosition.magnitude > dragRdius)
        {
            // 设置摇杆最远的位置
            contentPosition = contentPosition.normalized * dragRdius;
            SetContentAnchoredPosition(contentPosition);
        }

        // 最后 v2.x/y 就跟 Input中的 Horizontal Vertical 获取的值一样 
        v2 = content.anchoredPosition.normalized;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        v2 = Vector2.zero;
    }
    private void Update()
    {
        if (OnDragEvent != null)
            OnDragEvent(v2);
    }

    public void FindPlayer()
    {
        mc = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioControl>();
        OnDragEvent += mc.SetDrag;
    }
}
