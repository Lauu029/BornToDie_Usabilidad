using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline_Minion : MonoBehaviour
{
    [HideInInspector]
    public float trampolineJumpForce = 20;

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
                    collision.gameObject.GetComponentInParent<BasicMovement>().GoUp(trampolineJumpForce); // Aplicar fuerza de salto
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Chicken"))
        {
            Debug.Log("TOUCH CHICKEN");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, trampolineJumpForce);
        }
    }
}
