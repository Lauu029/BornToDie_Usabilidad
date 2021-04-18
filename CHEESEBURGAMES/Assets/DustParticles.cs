using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticles : MonoBehaviour
{

    [SerializeField]
    Transform particlePoint;

    public void particleWalk()
    {
        FindObjectOfType<ParticleManager>().PlayParticleWithThisDirectionAndScale("Dust", particlePoint.position, transform.localScale.x, 0.7f);
    }

    public void particleJump()
    {
        FindObjectOfType<ParticleManager>().PlayParticleWithThisDirectionAndScale("Dust", particlePoint.position, transform.localScale.x, 1);
    }

    private void Update()
    {
        //Debug.Log("transform.localScale.x = " + transform.localScale.x);
    }
}
