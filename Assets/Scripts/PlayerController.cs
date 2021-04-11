using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Collider2D crouchcoll;
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public float speed;
    public float jumpForce;
    public LayerMask ground;

    public int maxJumpCount = 2;
    private int jumpCount;

    public int cherryCount;
    public Text cherryNumber;

    private bool isHurt;
    //public bool isGround, isJump;
    //bool jumpPressed;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        crouchcoll = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {

        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();




        //if (Input.GetButtonDown("Jump") && jumpCount > 0)
        //{
        //    jumpPressed = true;

        //}



    }


    //void GroundMovement()
    //{
    //    float horizontalMove = Input.GetAxisRaw("Horizontal");
    //    rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

    //    if (horizontalMove !=0)
    //    {
    //        transform.localScale = new Vector3(horizontalMove, 1, 1);
    //    }

    //}

    //void Jump()
    //{

    //}




    void Movement()//角色移动
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //角色水平移动
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));

        }

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //角色跳跃
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anim.SetBool("jumping", true);
            jumpCount--;

        }




        //角色下蹲
        if (Input.GetKey(KeyCode.S) && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("crouching", true);
            crouchcoll.enabled = false;
        }
        else
        {
            anim.SetBool("crouching", false);
            crouchcoll.enabled = true;
        }

    }

    void SwitchAnim()//动画改变
    {
        anim.SetBool("idle", false);

        if (rb.velocity.y<0.1f  && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0f);
            if (Mathf.Abs(rb .velocity.x)<0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", false);

                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            jumpCount = maxJumpCount;   //重置跳跃数
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//物品收集
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            cherryCount++;
            cherryNumber.text = cherryCount.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//消灭敌人
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.tag == "Enemy")
        {
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                //执行一次跳跃
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            //左侧碰撞
            else if (transform.position.x< collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10f, rb.velocity.y);
                isHurt = true;
            }
            //右侧碰撞
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10f, rb.velocity.y);
                isHurt = true;

            }
        }


    }


}

