using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Chicken"))
        {
            FindObjectOfType<AudioManager>().Play("Button", 1);

            GameManager.GetInstance().NextLevel();
        }
    }
}
