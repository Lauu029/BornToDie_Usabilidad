using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveCaja : MonoBehaviour
{
    public GameObject caja;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 7)
        {
            Destroy(caja);
        }
    }
    
}
