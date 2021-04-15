using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public int currentLevel  = 1; // Ultimo nivel desbloqueado
    [HideInInspector]
    public int levelPlaying;
    [HideInInspector]
    public int numberOfLevels = 3;

    [SerializeField]
    GameObject rabbitTransition;
    [SerializeField]
    GameObject levelTransition;

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
        currentLevel = 1;
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

        // Transition
        GameObject newRabbitTransition = Instantiate(rabbitTransition, transform);
        Destroy(newRabbitTransition, 3);
    }

    public void ChangeLevel(int levelInt) // Solo se llama desde el menu
    {
        Debug.Log("levelInt = " + levelInt);
        Debug.Log("currentLevel = " + currentLevel);

        if (levelInt <= currentLevel)
        {
            levelPlaying = levelInt;
            string sceneName = "Level_" + levelInt;
            Debug.Log("sceneName = " + sceneName);//
            AudioManager.instance.ChangeBackgroundMusic(sceneName);
            SceneManager.LoadScene(sceneName);

            // Transition
            GameObject newLevelTransition = Instantiate(levelTransition, transform);
            Destroy(newLevelTransition, 3);
        }
    }

    public void NextLevel()
    {
        levelPlaying++;
         
        if (levelPlaying == numberOfLevels + 1) ChangeScene("Win"); // Si se ha llegado al ultimo nivel

        // Si no se ha llegado al ultimo nivel, seguir ejecutando este codigo

        // Si se ha completado el nivel por primera vez, sumarle 1 al "current level"
        if (levelPlaying > currentLevel)
            currentLevel = levelPlaying;

        // Cargar siguiente nivel
        ChangeLevel(levelPlaying);
    }
}
