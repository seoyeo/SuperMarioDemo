using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioControl : MonoBehaviour
{

    public enum MarioState
    {
        idle,
        run,
        jump,
        deth,
        paused
    }
    public MarioState state;

    [SerializeField]
    GameObject rightRay;
    [SerializeField]
    GameObject leftRay;

    delegate void RelifeHander(Vector3 pos);
    event RelifeHander RelifeEvent;

    int runID = Animator.StringToHash("run");
    int jumpID = Animator.StringToHash("jump");
    int dethID = Animator.StringToHash("deth");

    string keyForward = "d";
    string keyBack = "a";
    string keyJump = "space";
    float DRight;

    Animator anim;
    Rigidbody2D rb;
    CapsuleCollider2D bc;

    GameManager gameManager;

    float runSpeed;
    float height;
    float masss;
    bool isGroung;
    bool isFaceRight;
    private bool isBig;

    private bool rightButton;
    private bool leftButton;
    bool mobileJump;

    // Use this for initialization
    void Start()
    {
        runSpeed = 500f;
        height = 500f;
        masss = 2;
        isFaceRight = true;
        isBig = false;
        mobileJump = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<CapsuleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();

        RelifeEvent = gameManager.ReLife;

    }



    // Update is called once per frame
    void Update()
    {
        if (state == MarioState.paused)
            return;

        if (state == MarioState.deth)
        {
            Deth();
            return;
        }
        //if (mobileJump&& isGroung)
        //    Jump();
        if (state != MarioState.deth)
        {
            CheckIsGround();
            ChangeRigidbody();
            Run();
            Jump();

            if (state == MarioState.run)
                anim.SetBool(runID, true);
            else anim.SetBool(runID, false);
            if (state == MarioState.jump)
                anim.SetBool(jumpID, true);
            else anim.SetBool(jumpID, false);
        }

    }

    /// <summary>
    /// Run
    /// </summary>
    void Run()
    {
        //DRight = (Input.GetKey(keyForward) ? 1.0f : 0) - (Input.GetKey(keyBack) ? 1.0f : 0);
        if (DRight > 0)
        {
            if (!isFaceRight)
            {
                Reverse();
                isFaceRight = true;
            }
            rb.AddForce(Vector2.right * runSpeed * Time.deltaTime);
        }
        else if (DRight < 0)
        {
            if (isFaceRight)
            {
                Reverse();
                isFaceRight = false;
            }
            rb.AddForce(Vector2.left * runSpeed * Time.deltaTime);
        }
        if (DRight == 0) state = MarioState.idle;
        if (DRight != 0 && isGroung) state = MarioState.run;
    }
    /// <summary>
    /// 接收移动端的run
    /// </summary>
    /// <param name="drag"></param>
    public void SetDrag(Vector2 drag)
    {
        DRight = drag.x;
    }
    /// <summary>
    /// 转向
    /// </summary>
    void Reverse()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    /// <summary>
    /// 跳跃
    /// </summary>
    public void Jump()
    {
        if (isGroung && mobileJump)
        {
            mobileJump = false;
            rb.AddForce(Vector2.up * height);
            AudioManager.audioManager.PlayerVoice("Jump");
            state = MarioState.jump;
        }
    }
    /// <summary>
    /// 移动端的Jump
    /// </summary>
    public void MobileJump()
    {
        if (isGroung)
        {
            mobileJump = true;
        }
    }
    /// <summary>
    /// 检测Mario是否在地面
    /// </summary>
    void CheckIsGround()
    {
        RaycastHit2D rightRayHit;
        RaycastHit2D leftRayHit;
        rightRayHit = Physics2D.Raycast(rightRay.transform.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Default"));
        leftRayHit = Physics2D.Raycast(leftRay.transform.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Default"));

        if (rightRayHit.collider != null || leftRayHit.collider != null)
            isGroung = true;
        else if (rightRayHit.collider == null || leftRayHit.collider == null)
            isGroung = false;
    }
    /// <summary>
    /// 更改重力
    /// </summary>
    void ChangeRigidbody()
    {
        if (!isGroung)
            rb.gravityScale = masss;
        else rb.gravityScale = 1;
    }
    public void SmallJump()
    {
        rb.AddForce(Vector2.up * (height / 2f));
    }
    /// <summary>
    /// 死亡
    /// </summary>
    public void Deth()
    {
        anim.SetBool(dethID, true);
        bc.enabled = false;
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > 1 && info.IsName("deth"))
        {
            if (RelifeEvent != null)
            {
                RelifeEvent(transform.position);
                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// 碰到敌人
    /// </summary>
    public void CollisionEnemy()
    {
        SmallJump();
        if (!isBig)
        {
            state = MarioState.deth;
            AudioManager.audioManager.PlayerVoice("Deth");
        }

        if (isBig)
        {
            ChangeSmall();
        }
    }

    public void SetDeth()
    {
        SmallJump();
        state = MarioState.deth;
    }
    /// <summary>
    /// 变大
    /// </summary>
    public void ChangeBig()
    {
        var scale = new Vector3(transform.localScale.x * 1.47f, transform.localScale.y * 1.47f, transform.localScale.z * 1.47f);
        transform.localScale = scale;
        isBig = true;
    }

    /// <summary>
    /// 变小
    /// </summary>
    void ChangeSmall()
    {
        var scale = new Vector3(transform.localScale.x / 1.47f, transform.localScale.y / 1.47f, transform.localScale.z / 1.47f);
        transform.localScale = scale;
        isBig = false;
    }

}
