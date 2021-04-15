using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveCaja : MonoBehaviour
{
    public GameObject caja;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            FindObjectOfType<AudioManager>().Play("Key", 1);

            Destroy(caja);
            Destroy(this.gameObject);
        }
    }
}
