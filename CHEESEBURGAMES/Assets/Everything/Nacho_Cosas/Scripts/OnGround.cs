using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    [HideInInspector]
    public bool touchingGround;
    [HideInInspector]
    public Transform objTouching;

    // CONFIGURACION
    BasicMovement basicMov;

    private void Awake()
    {
        basicMov = GetComponentInParent<BasicMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        basicMov.ChangeOnGround(true);

        touchingGround = true;
        objTouching = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("JUMP");

        basicMov.ChangeOnGround(false);

        touchingGround = false;
        objTouching = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        basicMov.ChangeOnGround(true);
    }
}
