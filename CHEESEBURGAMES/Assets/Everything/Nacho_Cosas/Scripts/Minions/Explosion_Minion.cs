using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion_Minion : MonoBehaviour
{

    [Header("VALORES MODIFICABLES")]
    [SerializeField]
    float timeBeforeExploding;
    [SerializeField]
    float explosionRadius;

    // CONFIGURACION
    Text text;

    // VARIABLES
    float time;
    bool startedMoving; // Indica si el minion se ha empezado a mover

    private void Awake()
    {
        time = timeBeforeExploding;
        startedMoving = false;
        text = GetComponentInChildren<Text>();
        text.text = time.ToString("f0");
    }

    private void Update()
    {
        Explosion();
    }

    void Explosion()
    {
        if (!startedMoving && (Input.GetButtonDown("Jump") || Input.GetKeyDown("w") || Input.GetAxisRaw("Horizontal") != 0))
        {
            startedMoving = true;
            text.color = Color.red;
        }

        if (startedMoving)
        {
            text.text = time.ToString("f0"); // Actualizar texto

            if (time - Time.deltaTime <= 0) Explode();
            else time -= Time.deltaTime;
        }
    }

    void Explode()
    {
        Collider2D allCollisions = Physics2D.OverlapCircle(transform.position, explosionRadius, LayerMask.GetMask("DestructibleWall"));

        if (allCollisions != null)
            Destroy(allCollisions.gameObject);

        FindObjectOfType<AudioManager>().Play("Explosion", 1);
        FindObjectOfType<ParticleManager>().PlayParticle("Explosion", transform.position);

        GetComponent<BasicMovement>().Die();
    }
}
