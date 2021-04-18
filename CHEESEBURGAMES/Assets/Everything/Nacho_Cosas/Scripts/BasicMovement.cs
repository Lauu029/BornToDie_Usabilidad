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

    Animator gfxAnimator;

    // Configuracion
    Rigidbody2D rb;
    bool onGround;

    
    DustParticles dustParticles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        gfxAnimator = gfx.GetComponent<Animator>();

        dustParticles = GetComponentInChildren<DustParticles>();

        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        LateralMovement();

        CheckJump();

        // ActualizarAnimator
        gfxAnimator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
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

    void CheckJump()
    {
        if ((Input.GetKeyDown("w") || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && onGround)
        {
            dustParticles.particleJump();
            GoUp(jumpForce);
            FindObjectOfType<AudioManager>().Play("Jump", 1);
        }
    }

    public void GoUp(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
    }

    public void ChangeOnGround(bool isOnGround)
    {
        onGround = isOnGround;
        gfxAnimator.SetBool("IsJumping", !onGround);
    }

    public void OnDisable()
    {
        gfxAnimator.SetFloat("Speed", 0);
        gfxAnimator.SetBool("IsJumping", false);

        //if (GetComponent<Explosion_Minion>() == null && GetComponent<Trampoline_Minion>() == null) // Es mini
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Death", 1);
        FindObjectOfType<ParticleManager>().PlayParticle("Death", transform.position);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Trampoline_Minion>() == null && GetComponent<Explosion_Minion>() == null)
            if (collision.gameObject.GetComponent<ChickenMovement>() != null) Die();
    }
}
