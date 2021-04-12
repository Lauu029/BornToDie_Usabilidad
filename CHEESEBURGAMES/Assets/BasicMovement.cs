using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    // Valores modificables
    [SerializeField]
    float runVelocity;
    float jumpForce;

    // Configuracion
    Rigidbody2D rb;
    OnGround onG;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        onG = GetComponentInChildren<OnGround>();
    }
    private void Update()
    {
        
    }
}
