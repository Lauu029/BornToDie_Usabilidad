using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //comprueba si est chocando con una pared, si es asi avisa a la gallina de que gire
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.collider.gameObject.layer == LayerMask.NameToLayer("DestructibleWall"))
        {
            Debug.Log("Pared tocada");
            GetComponentInParent<ChickenMovement>().Switch();
        }
    }
}
