using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    private float startPoint;

    void Start()
    {
        startPoint = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector2(startPoint + cam.position.x * moveRate, transform.position.y);
    }
}
