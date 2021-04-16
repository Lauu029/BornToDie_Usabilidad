using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        // InitialTransition
        GameObject newRabbitTransition = Instantiate(rabbitTransition, transform);
        Destroy(newRabbitTransition, 3);
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
        StartCoroutine(ChangeSceneEnumerator(sceneName));
    }

    IEnumerator ChangeSceneEnumerator(string sceneName)
    {
        GameObject newRabbitTransition = Instantiate(rabbitTransition, transform);
        newRabbitTransition.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSecondsRealtime(2.5f);
        Destroy(newRabbitTransition);

        AudioManager.instance.ChangeBackgroundMusic(sceneName);
        SceneManager.LoadScene(sceneName);

        // Transition
        newRabbitTransition = Instantiate(rabbitTransition, transform);
        Destroy(newRabbitTransition, 3f);
    }




    public void ChangeLevel(int levelInt) // Solo se llama desde el menu
    {
        StartCoroutine(ChangeLevelEnumerator(levelInt));
    }

    IEnumerator ChangeLevelEnumerator(int levelInt)
    {
        if (levelInt <= currentLevel)
        {
            GameObject newLevelTransition = Instantiate(levelTransition, transform);
            newLevelTransition.GetComponentInChildren<Text>().text = "LEVEL " + levelInt;
            newLevelTransition.GetComponent<Animator>().SetTrigger("Start");

            yield return new WaitForSecondsRealtime(3f);
            Destroy(newLevelTransition);

            levelPlaying = levelInt;
            string sceneName = "Level_" + levelInt;
            AudioManager.instance.ChangeBackgroundMusic(sceneName);
            SceneManager.LoadScene(sceneName);

            // Transition
            newLevelTransition = Instantiate(levelTransition, transform);
            newLevelTransition.GetComponentInChildren<Text>().text = "LEVEL " + levelInt;
            Destroy(newLevelTransition, 3.1f);
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
