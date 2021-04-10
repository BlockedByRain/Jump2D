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

    //public bool isGround, isJump;
    //bool jumpPressed;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        crouchcoll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
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
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            jumpCount = maxJumpCount;   //重置跳跃数
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)//物品收集
    {
        if (collider2D.tag == "Collection")
        {
            Destroy(collider2D.gameObject);
            cherryCount++;
            cherryNumber.text = cherryCount.ToString();
        }
    }





}

