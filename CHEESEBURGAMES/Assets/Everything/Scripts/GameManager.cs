using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void ChangeScene(string sceneName)
    {
        AudioManager.instance.ChangeBackgroundMusic(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
