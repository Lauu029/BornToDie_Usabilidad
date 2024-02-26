using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transtion;
    public float transitionTime=1f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
           
        }
        
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transtion.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
