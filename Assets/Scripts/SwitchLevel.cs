using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    public GameObject player;
    //关卡切换
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.GetComponent<PlayerController>().congratulation)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

    }
}
