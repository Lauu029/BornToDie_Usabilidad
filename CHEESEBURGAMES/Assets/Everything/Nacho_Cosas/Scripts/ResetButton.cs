using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ResetButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool onPointer;
    Animator thisAnimator;

    private void Awake()
    {
        thisAnimator = GetComponent<Animator>();

        Debug.Log("lskmcfvhbdjc");
    }

    private void Update()
    {
        if (onPointer && Input.GetMouseButtonDown(0))
        {
            GameManager.GetInstance().ChangeLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //POINTER ENTER/EXIT // Quitar los dos metodos siguientes en el caso de que se quiera un menu solo controlado por input de teclado/mando
    public void OnPointerEnter(PointerEventData eventData)
    {
        thisAnimator.SetBool("Selected", true);
        GetComponentInChildren<Text>().color = Color.red;

        onPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        thisAnimator.SetBool("Selected", false);
        GetComponentInChildren<Text>().color = Color.black;

        onPointer = false;
    }
}
