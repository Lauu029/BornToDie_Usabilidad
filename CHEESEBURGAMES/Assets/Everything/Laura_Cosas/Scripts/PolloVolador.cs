using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloVolador : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontal, vertical;

    Vector2 movimiento;

    public float velocidad;

    Animator thisAnimator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(horizontal, vertical);
        Debug.Log("movimiento = " + movimiento);
        movimiento.Normalize();

        thisAnimator.SetFloat("Speed", movimiento.magnitude);

        if (movimiento.x > 0) thisAnimator.transform.localScale = new Vector3(1, 1, 0);
        else if (movimiento.x < 0) thisAnimator.transform.localScale = new Vector3(-1, 1, 0);
    }
    private void FixedUpdate()
    {
        if (movimiento.x != 0)
            rb.velocity = new Vector2(movimiento.x * velocidad, rb.velocity.y);
        if (movimiento.y != 0)
            rb.velocity = new Vector2(rb.velocity.x, movimiento.y * velocidad);
    }


}
