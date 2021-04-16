using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    [SerializeField]
    float chickenSpeed;
    Rigidbody2D rb;
    bool movesRight;

    [SerializeField]
    Transform gfx;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movesRight = true;
    }

    private void Update()
    {
        gfx.GetComponent<Animator>().SetFloat("Yvelocity", rb.velocity.y);
        gfx.GetComponent<Animator>().SetFloat("Xvelocity", rb.velocity.x);
    }

    void FixedUpdate()
    {
        Vector2 dir;

        if (movesRight) dir = new Vector2(chickenSpeed, rb.velocity.y);
        else dir = new Vector2(-chickenSpeed, rb.velocity.y);

        rb.velocity = dir;
    }
    public void Switch() //Cambiar sprite y direccion
    {
        /*LLega a un limite mientras se mueve a la derecha, rota y se mueve hacia la izquierda 
        hasta que se ejecute de nuevo y movesright sea false, en cuyo caso rotara, cambiara sprite y pondra movesrght true*/

        gfx.transform.localScale = new Vector3(-gfx.transform.localScale.x, gfx.transform.localScale.y, 0);

        movesRight = !movesRight;

        //if (movesRight)
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);

        //    if (transform.rotation.y > 0)
        //    {
        //        movesRight = false;
        //    }
        //}
        //else
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);

        //    if (transform.rotation.y <180)
        //    {
        //        movesRight = true;
        //    }
        //}
    }
}
