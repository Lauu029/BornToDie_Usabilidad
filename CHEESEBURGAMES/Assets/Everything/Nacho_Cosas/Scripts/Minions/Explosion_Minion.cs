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
        text.text = time.ToString();
    }

    private void Update()
    {
        Explosion();
    }

    void Explosion()
    {
        if (!startedMoving && (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")))
            startedMoving = true;

        if (startedMoving)
        {
            if (time - Time.deltaTime <= 0) Explode();
            else time -= Time.deltaTime;
        }
    }

    void Explode()
    {
        if (Physics2D.OverlapCircle(transform.position, explosionRadius, LayerMask.GetMask("DestructibleWall") ) )
        {

        }
    }
}
