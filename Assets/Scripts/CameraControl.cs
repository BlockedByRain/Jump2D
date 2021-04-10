using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;



    // Update is called once per frame
    void Update()
    {
        //摄像头跟随角色
        transform.position = new Vector3(player.position.x, player.position.y, -10f);
    }
}
