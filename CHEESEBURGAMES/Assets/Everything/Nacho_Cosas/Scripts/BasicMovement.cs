using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [Header("VALORES MODIFICABLES")]
    [SerializeField]
    float lateralVelocity;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float gravityScale;

    [Header("REFERENCIAS")]
    [SerializeField]
    Transform gfx;

    // Configuracion
    Rigidbody2D rb;
    OnGround onG;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        onG = GetComponentInChildren<OnGround>();

        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        LateralMovement();

        CheckForTrampoline();

        Jump();
    }

    void LateralMovement()
    {
        // Move
        float xAxis = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xAxis * lateralVelocity, rb.velocity.y);

        // Actualizar direccion del sprite
        if (rb.velocity.x > 0) gfx.localScale = new Vector2(1, 1);
        else if (rb.velocity.x < 0) gfx.localScale = new Vector2(-1, 1);
    }

    void Jump()
    {
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && onG.touchingGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void CheckForTrampoline() // Comprueba si se ha colisionado con un trampolin, y en tal, caso aplica la fuerza necesaria
    {
        Trampoline_Minion trampolineScript = null;

        if (onG.objTouching != null)
            trampolineScript = onG.objTouching.GetComponent<Trampoline_Minion>();

        if (trampolineScript != null)
            rb.velocity = new Vector2(rb.velocity.x, trampolineScript.trampolineJumpForce);
    }
}
