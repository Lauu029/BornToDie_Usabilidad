using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WInMenu : MonoBehaviour
{
    bool canSkip = false;

    private void Awake()
    {
        canSkip = false;
        Invoke("CanSkip", 4);
    }

    void CanSkip()
    {
        canSkip = true;
    }

    private void Update()
    {
        if (canSkip && Input.anyKeyDown)
            SceneManager.LoadScene("MainMenu");
    }
}
