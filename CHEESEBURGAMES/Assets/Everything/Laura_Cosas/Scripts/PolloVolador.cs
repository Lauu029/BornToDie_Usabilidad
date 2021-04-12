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
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movimiento = new Vector2(horizontal, vertical);
        movimiento.Normalize();
    }
    private void FixedUpdate()
    {
        if (movimiento.magnitude != 0)
        {
            rb.velocity = movimiento * velocidad;
        }
       
    }


}
