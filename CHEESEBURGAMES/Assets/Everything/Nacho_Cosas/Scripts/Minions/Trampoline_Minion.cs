using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline_Minion : MonoBehaviour
{
    [HideInInspector]
    float trampolineJumpForce = 18;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BasicMovement>() != null)
        {
            Vector2 colPos = collision.transform.position;
            if (colPos.y > transform.position.y + 0.1f) // Comprobar si el Minion con el que ha interactuado esta por encima suyo
                if (colPos.x > transform.position.x - transform.localScale.x/2 && colPos.x < transform.position.x + transform.localScale.x / 2) // Comprobar si no esta lejos
                    collision.GetComponentInParent<BasicMovement>().GoUp(trampolineJumpForce); // Aplicar fuerza de salto
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BasicMovement>() != null)
        {
            Vector2 colPos = collision.transform.position;
            if (colPos.y > transform.position.y + 0.1f) // Comprobar si el Minion con el que ha interactuado esta por encima suyo
                if (colPos.x > transform.position.x - transform.localScale.x / 2 && colPos.x < transform.position.x + transform.localScale.x / 2) // Comprobar si no esta lejos
                {
                    FindObjectOfType<AudioManager>().Play("Jump", 0.5f);

                    if (collision.gameObject.GetComponentInParent<BasicMovement>() != null)
                        collision.gameObject.GetComponentInParent<BasicMovement>().GoUp(trampolineJumpForce); // Aplicar fuerza de salto
                    else if (collision.gameObject.layer == LayerMask.NameToLayer("Key"))
                        collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, trampolineJumpForce);
                }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Chicken") || collision.gameObject.layer == LayerMask.NameToLayer("Key"))
        {
            if (collision.transform.position.y > transform.position.y + 0.2f)
            {
                FindObjectOfType<AudioManager>().Play("Jump", 0.5f);
                Debug.Log("TOUCH CHICKEN/KEY");
                if (collision.gameObject.layer == LayerMask.NameToLayer("Chicken"))
                    GetComponent<BasicMovement>().Die();
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, trampolineJumpForce);
            }
        }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Key"))
        //{
        //    Debug.Log("TOUCH KEY");
        //    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, trampolineJumpForce);
        //}
    }

    private void Update()
    {
        Debug.Log("trampolineJumpForce = " + trampolineJumpForce);
    }
}
