using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public int currentLevel  = 6; // Ultimo nivel desbloqueado
    [HideInInspector]
    public int levelPlaying = 1;
    [HideInInspector]
    int numberOfLevels = 6;

    [SerializeField]
    GameObject rabbitTransition;
    [SerializeField]
    GameObject levelTransition;

    public bool usingController;

    public bool usingCoroutine;

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

        PlayerPrefs.SetInt("KurrentLevel", 6);
        //currentLevel = PlayerPrefs.GetInt("KurrentLevel", 6);

        // InitialTransition
        GameObject newRabbitTransition = Instantiate(rabbitTransition, transform);
        Destroy(newRabbitTransition, 3);
    }

    private void Update()
    {
        //Debug.Log("numberOfLevels = " + numberOfLevels);
        //Debug.Log("levelPlaying = " + levelPlaying);
        //Debug.Log("currentLevel = " + currentLevel);
        //if (Input.GetKeyDown("k"))
        //{
        //    Debug.Log("k");
        //    NextLevel();
        //} else if (Input.GetKeyDown("l"))
        //{
        //    SceneManager.LoadScene("MainMenu");
        //}
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void ChangeScene(string sceneName)
    {
        if (!usingCoroutine)
            StartCoroutine(ChangeSceneEnumerator(sceneName));
    }

    IEnumerator ChangeSceneEnumerator(string sceneName)
    {
        usingCoroutine = true;
        GameObject newRabbitTransition = Instantiate(rabbitTransition, transform);
        newRabbitTransition.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSecondsRealtime(2.5f);
        Destroy(newRabbitTransition);

        AudioManager.instance.ChangeBackgroundMusic(sceneName);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);

        if (FindObjectOfType<ControllerConnected>() != null)
            FindObjectOfType<ControllerConnected>().SetUpUsingController(usingController);
        // Transition
        newRabbitTransition = Instantiate(rabbitTransition, transform);
        Destroy(newRabbitTransition, 3f);
        Invoke("NoCoroutine", 1.5f);
    }

    void NoCoroutine()
    {
        usingCoroutine = false;
    }

    public void ChangeLevel(int levelInt) // Solo se llama desde el menu
    {
        if (!usingCoroutine)
            StartCoroutine(ChangeLevelEnumerator(levelInt));
    }

    IEnumerator ChangeLevelEnumerator(int levelInt)
    {
        usingCoroutine = true;

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
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneName);

            if (FindObjectOfType<ControllerConnected>() != null)
                FindObjectOfType<ControllerConnected>().SetUpUsingController(usingController);
            // Transition
            newLevelTransition = Instantiate(levelTransition, transform);
            newLevelTransition.GetComponentInChildren<Text>().text = "LEVEL " + levelInt;
            Destroy(newLevelTransition, 3.1f);
        }
        Invoke("NoCoroutine", 2);
    }

    public void NextLevel()
    {
        if (!usingCoroutine)
        {
            levelPlaying++;

            if (levelPlaying == numberOfLevels + 1) ChangeScene("Win"); // Si se ha llegado al ultimo nivel

            // Si no se ha llegado al ultimo nivel, seguir ejecutando este codigo

            // Si se ha completado el nivel por primera vez, sumarle 1 al "current level"
            if (levelPlaying > currentLevel)
            {
                currentLevel = levelPlaying;
                PlayerPrefs.SetInt("KurrentLevel", currentLevel);
            }

            // Cargar siguiente nivel
            ChangeLevel(levelPlaying);
        }
    }
}
