using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    public bool touchingGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchingGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingGround = false;
    }
}
