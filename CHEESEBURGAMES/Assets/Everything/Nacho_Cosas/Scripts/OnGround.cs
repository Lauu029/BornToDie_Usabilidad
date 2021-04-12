using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    public bool touchingGround;
    public Transform objTouching;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchingGround = true;
        objTouching = collision.transform;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingGround = false;
        objTouching = null;
    }
}
