using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel = 0; // Ultimo nivel desbloqueado
    public int levelPlaying;
    public int numberOfLevels = 3;

    private void Awake()
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

        // SIMULACION PLAYERPREF
        currentLevel = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("k");
            NextLevel();
        } else if (Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene("MainMenu");
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

    public void ChangeLevel(int levelInt) // Solo se llama desde el menu
    {
        if (levelInt <= currentLevel)
        {
            levelPlaying = levelInt;
            string sceneName = "Level_" + levelInt;
            AudioManager.instance.ChangeBackgroundMusic(sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }

    public void NextLevel()
    {
        levelPlaying++;
         
        if (levelPlaying == numberOfLevels) SceneManager.LoadScene("Win"); // Si se ha llegado al ultimo nivel

        // Si no se ha llegado al ultimo nivel, seguir ejecutando este codigo

        // Si se ha completado el nivel por primera vez, sumarle 1 al "current level"
        if (levelPlaying > currentLevel)
            currentLevel = levelPlaying;

        // Cargar siguiente nivel
        ChangeLevel(levelPlaying);
    }
}
