using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio= GetComponent<AudioSource>();

    }

    public void Death()//销毁对象
    {
        deathAudio.Play();
        Destroy(gameObject);

    }

    public void JumpOn()//触发死亡动画
    {
        deathAudio.Play();//播放死亡声音
        anim.SetTrigger("death");
    }

}
