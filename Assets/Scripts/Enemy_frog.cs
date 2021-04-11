using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy
{
    public Transform leftPoint, rightPoint;
    public float speed,jumpForce;
    public LayerMask ground;

    private Rigidbody2D rb;
    //private Animator anim;
    private Collider2D coll;
    private float leftX, rightX;
    private bool faceLeft = true;



    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        transform.DetachChildren();
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

    }

    void Update()
    {
        SwitchAnim();
    }


    void Movement()//移动
    {
        if (faceLeft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }        
            if (transform.position.x < leftX)
            {
                rb.velocity = new Vector2(speed, jumpForce);
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            if (transform.position.x > rightX)
            {
                rb.velocity = new Vector2(-speed, jumpForce);
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }


    void SwitchAnim()//动画改变
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y<0.1f)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }



}
