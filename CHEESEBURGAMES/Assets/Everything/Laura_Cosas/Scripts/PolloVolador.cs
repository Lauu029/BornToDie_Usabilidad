using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloVolador : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontal, vertical;

    Vector2 movimiento;

    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(horizontal, vertical);
        Debug.Log("movimiento = " + movimiento);
        movimiento.Normalize();

    }
    private void FixedUpdate()
    {
        if (movimiento.x != 0)
            rb.velocity = new Vector2(movimiento.x * velocidad, rb.velocity.y);
        if (movimiento.y != 0)
            rb.velocity = new Vector2(rb.velocity.x, movimiento.y * velocidad);
    }


}
