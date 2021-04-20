using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManagerInstance;
    public AudioSource audioSource;
    public AudioClip jumpAudio, hurtAudio, cherryAudio;


    private void Awake()
    {
        soundManagerInstance = this;
    }


    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }

    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();

    }
    public void CherryAudio()
    {
        audioSource.clip = cherryAudio;
        audioSource.Play();

    }
}
