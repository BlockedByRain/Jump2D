using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_opossum : Enemy
{
    private Rigidbody2D rb;
    public Transform leftPoint, rightPoint;
    public float speed;
    private float leftX, rightX;
    private bool faceLeft = true;



    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();      
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        transform.DetachChildren();
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

    }

    void Update()
    {
        Movement();
    }


    void Movement()//移动
    {
        if (faceLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

}
