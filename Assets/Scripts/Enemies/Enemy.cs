using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum EnemnState
    {
        run,
        sleep,
        die
    }

    [SerializeField]
    GameObject origin;

    public EnemnState state;
    public Rigidbody2D rb;
    public Animator anim;
    public float speed;
    public bool isFaceLeft;
    public bool isSleep;

    // Use this for initialization
    void Start()
    {
        //isFaceLeft = true;
        //speed = 10;

        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChenckObstacle();
        Run();
    }

    /// <summary>
    /// 控制行走
    /// </summary>
    public void Run()
    {
        if (isFaceLeft)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            rb.AddForce(Vector2.right * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 检测是否有障碍物
    /// </summary>
    public void ChenckObstacle()
    {
        RaycastHit2D hit;
        if (isFaceLeft)
            hit = Physics2D.Raycast(origin.transform.position, Vector2.left, 0.1f, 1 << LayerMask.NameToLayer("Default"));
        else
            hit = Physics2D.Raycast(origin.transform.position, Vector2.right, 0.1f, 1 << LayerMask.NameToLayer("Default"));
        if (hit.collider != null && hit.collider.tag != "Player")
        {
            isFaceLeft = !isFaceLeft;
        }
        if (hit.collider != null && hit.collider.tag != "Player"&&hit.collider.tag =="MushroomEnemy"&&isSleep)
        {
            isFaceLeft = !isFaceLeft;
        }
    }
}
