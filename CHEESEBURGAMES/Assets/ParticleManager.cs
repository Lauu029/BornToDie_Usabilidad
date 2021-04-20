using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance;

    GameObject[] allParticles;

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        allParticles = new GameObject[transform.childCount];
        for (int i = 0; i < allParticles.Length; i++)
        {
            allParticles[i] = transform.GetChild(i).gameObject;
            allParticles[i].SetActive(false);
        }
    }

    public void PlayParticle(string particleName, Vector3 particlePosition)
    {
        GameObject p = Array.Find(allParticles, gameObject => gameObject.name == particleName);

        p.SetActive(true);
        GameObject newParticle = Instantiate(p, particlePosition, Quaternion.identity);
        p.SetActive(false);
        Destroy(newParticle, 5);
    }

    public void InstantiateConstantParticleInThis(string particleName, Vector3 particlePosition, Transform inThisTransform)
    {
        GameObject p = Array.Find(allParticles, gameObject => gameObject.name == particleName);

        p.SetActive(true);
        GameObject newParticle = Instantiate(p, particlePosition, Quaternion.identity, inThisTransform);
        p.SetActive(false);
    }

    public void PlayParticleWithThisDirectionAndScale(string particleName, Vector3 particlePosition, float direction, float scale)
    {
        GameObject p = Array.Find(allParticles, gameObject => gameObject.name == particleName);

        p.SetActive(true);
        GameObject newParticle = Instantiate(p, particlePosition, Quaternion.identity);
        p.SetActive(false);
        newParticle.transform.GetChild(0).localScale = new Vector3(scale * direction, scale, 0);
        Destroy(newParticle, 5);
    }
}
