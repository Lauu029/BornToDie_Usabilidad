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

            BasicMovement[] allBasicMovement = FindObjectsOfType<BasicMovement>();
            for (int i = 0; i < allBasicMovement.Length; i++)
                allBasicMovement[i].Die();

            PolloVolador[] allPolloVolador = FindObjectsOfType<PolloVolador>();
            for (int i = 0; i < allPolloVolador.Length; i++)
                allPolloVolador[i].Die();

            GameManager.GetInstance().NextLevel();
        }
    }
}
