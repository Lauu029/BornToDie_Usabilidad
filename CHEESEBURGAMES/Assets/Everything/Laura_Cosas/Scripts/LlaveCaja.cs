using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveCaja : MonoBehaviour
{
    public GameObject caja;

    private void Awake()
    {
        FindObjectOfType<ParticleManager>().InstantiateConstantParticleInThis("Shiny", transform.position, transform);
        FindObjectOfType<ParticleManager>().InstantiateConstantParticleInThis("Shiny", caja.transform.position, caja.transform);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            FindObjectOfType<AudioManager>().Play("Key", 1);

            FindObjectOfType<ParticleManager>().PlayParticle("RainBow", transform.position);
            FindObjectOfType<ParticleManager>().PlayParticle("RainBow", caja.transform.position);

            Destroy(caja);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BasicMovement>() != null && collision.gameObject.GetComponent<BasicMovement>().enabled == true)
        {
            GetComponent<Rigidbody2D>().gravityScale = 8;
            GetComponent<Rigidbody2D>().drag = 8;
        } else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().drag = 4;
        }
    }
}
