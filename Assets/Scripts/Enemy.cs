using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;



    protected virtual void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void Death()//销毁对象
    {
        Destroy(gameObject);

    }

    public void JumpOn()//触发死亡动画
    {
        anim.SetTrigger("death");
    }

}
