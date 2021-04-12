using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;
    public GameObject text;
    private void OnTriggerEnter2D(Collider2D collider2D)//对话框显示
    {
        if (collider2D.tag =="Player")
        {
            enterDialog.SetActive(true);
            text.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)//对话框消失
    {
        if (collider2D.tag == "Player")
        {
            enterDialog.SetActive(false);
            text.SetActive(false);
        }
    }

}
