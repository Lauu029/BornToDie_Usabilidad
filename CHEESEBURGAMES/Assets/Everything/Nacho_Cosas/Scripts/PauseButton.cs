using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool onPointer;
    Animator thisAnimator;

    PauseMenuScript pauseMenu;

    private void Awake()
    {
        thisAnimator = GetComponent<Animator>();
        pauseMenu = FindObjectOfType<PauseMenuScript>();
    }

    private void Update()
    {
        if (onPointer && Input.GetMouseButtonDown(0))
        {
            pauseMenu.PauseSelect();
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
