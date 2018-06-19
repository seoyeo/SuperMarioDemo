using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MushroomControl : MonoBehaviour
{
    [SerializeField]
    GameObject groundRightRay;
    [SerializeField]
    GameObject groundLeftRay;
    [SerializeField]
    GameObject rightRay;
    [SerializeField]
    GameObject leftRay;
    int runID = Animator.StringToHash("run");
    public bool isGroung;
    public bool isOpen;
    public bool isRight;
    public float speed;

    Animator anim;
    Rigidbody2D rb;
    BoxCollider2D bc;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        bc.enabled = false;
        transform.parent = null;
        transform.DOMoveY(transform.position.y + 0.38f, 1f, false);
        isGroung = false;
        isRight = true;
        isOpen = false;
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        CheckObstacle();
        IsFinish();
        Run();
    }

    /// <summary>
    /// Run
    /// </summary>
    public void Run()
    {
        if (isGroung && isOpen)
        {
            anim.SetBool(runID, true);
            if (isRight)
                rb.AddForce(Vector2.right * speed);
            else if (!isRight)
                rb.AddForce(Vector2.left * speed);
        }
    }
    /// <summary>
    /// 判断是否在地面
    /// </summary>
    void CheckGround()
    {
        RaycastHit2D groundRightRayHit;
        RaycastHit2D groundLeftRayHit;
        groundRightRayHit = Physics2D.Raycast(groundRightRay.transform.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Default"));
        groundLeftRayHit = Physics2D.Raycast(groundLeftRay.transform.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Default"));

        if (groundRightRayHit.collider != null || groundLeftRayHit.collider != null)
            isGroung = true;
        else if (groundRightRayHit.collider == null || groundLeftRayHit.collider == null)
            isGroung = false;
    }
    /// <summary>
    /// open动画结束后才开始移动
    /// </summary>
    void IsFinish()
    {
        if (!isOpen)
        {
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime > 1f && info.IsName("open"))
            {
                bc.enabled = true;
                isOpen = true;
            }
        }
    }
    /// <summary>
    /// 检测前后是否有障碍物
    /// </summary>
    void CheckObstacle()
    {
        if (isOpen)
        {
            RaycastHit2D rightHit = Physics2D.Raycast(rightRay.transform.position, Vector2.right, 0.1f, 1 << LayerMask.NameToLayer("Default"));
            RaycastHit2D leftHit = Physics2D.Raycast(leftRay.transform.position, Vector2.left, 0.1f, 1 << LayerMask.NameToLayer("Default"));
            if (rightHit.collider != null && rightHit.collider.tag != "Player") isRight = false;
            if (leftHit.collider != null && leftHit.collider.tag != "Player") isRight = true;
        }
    }
}
