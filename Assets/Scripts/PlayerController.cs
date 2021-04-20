using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public Collider2D disColl;
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public int maxJumpCount = 2;
    private int jumpCount;
    public Transform cellingCheck,groundCheck;
    public int cherryCount;
    public Text cherryNumber;
    public AudioSource deadAudio;
    public bool congratulation = false;
    private bool isHurt;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();

    }
    void Update()
    {
        Jump();
        Crouch();

    }


    void Movement()//角色移动
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //角色水平移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));

        }

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }



    }

    void SwitchAnim()//动画改变
    {
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

                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            jumpCount = maxJumpCount;   //重置跳跃数
            anim.SetBool("falling", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//碰撞触发器
    {
        //角色死亡
        if (collision.tag=="DeadLine")
        {
            Death();
        }



        //物品收集
        if (collision.tag == "Collection")
        {
            SoundManager.soundManagerInstance.CherryAudio();
            Destroy(collision.gameObject);
            cherryCount++;
            if (cherryCount>=10)
            {
                congratulation = true;
            }
            cherryNumber.text = cherryCount.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//消灭敌人
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.tag == "Enemy")
        {
            if (anim.GetBool("falling") && collision.transform.position.y< transform.position.y)
            {
                enemy.JumpOn();
                //执行一次跳跃
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
                anim.SetBool("jumping", true);
            }
            //受伤
            //左侧碰撞
            else if (transform.position.x< collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10f, rb.velocity.y);
                SoundManager.soundManagerInstance.HurtAudio();
                isHurt = true;
            }
            //右侧碰撞
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                
                rb.velocity = new Vector2(10f, rb.velocity.y);
                SoundManager.soundManagerInstance.HurtAudio();
                isHurt = true;

            }
        }


    }


    void Crouch()//角色下蹲
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position,0.2f,ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                disColl.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                disColl.enabled = true;
            }
        }

    }


    void Restart()//重置游戏
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


    
    
    void Jump()
    {
        //角色跳跃
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            SoundManager.soundManagerInstance.JumpAudio();
            anim.SetBool("jumping", true);
            jumpCount--;

        }
    }


    public void Death()
    {
        coll.enabled = false;
        disColl.enabled = false;
        anim.SetTrigger("death");
        deadAudio.Play();
        coll.enabled = false;
        disColl.enabled = false;
        GetComponent<AudioSource>().enabled = false;
        Invoke("Restart", 2f);//重置游戏
    }


    
}

