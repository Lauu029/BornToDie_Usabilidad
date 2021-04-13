using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ASIGNACION (todo lo que se tenga que referenciar con "[SerialisedField]")

    // CONFIGURACION (todos los componentes de otros objetos que no necesiten "[SerialisedField]")
    MainMenuScript MainMenuScript;
    PauseMenuScript pauseMenuScript;
    LevelSelectorMenu levelSelectorMenu;
    Animator animator;

    // VARIABLES
    [HideInInspector]
    public int thisButtonIndex; // Almacena el Index de este boton, asignado por el "ButtonManager" al principio en el Awake
    bool onPointer;
    bool buttonSelected;

    void Awake()
    {
        // ASIGNAR VARIABLES
        animator = GetComponent<Animator>();

        // Asignar padrem dependiendo de si es un "MainMenu" o un "PauseMenu" o "levelSelectorMenu"
        if (GetComponentInParent<PauseMenuScript>() != null) pauseMenuScript = GetComponentInParent<PauseMenuScript>();
        else if (GetComponentInParent<LevelSelectorMenu>() != null) levelSelectorMenu = GetComponentInParent<LevelSelectorMenu>();
        else if (GetComponentInParent<MainMenuScript>() != null) MainMenuScript = GetComponentInParent<MainMenuScript>();

        // VALORES INICIALES

    }

    void Update()
    {
        // Presionar un boton si se hace click sobre el
        if (onPointer && Input.GetMouseButtonDown(0))
        {
            if (MainMenuScript != null) MainMenuScript.PressButton(thisButtonIndex);
            else if (pauseMenuScript != null) pauseMenuScript.PressButton(thisButtonIndex);
            else if (levelSelectorMenu != null) levelSelectorMenu.PressButton(thisButtonIndex);
        }
    }

    public void Select() // Se llama cuando este botón es seleccionado, ya sea a causa del cursor (ha entrado) o con input de teclado/mando
    {
        FindObjectOfType<AudioManager>().Play("Button", 1);

        if (animator != null)
            animator.SetBool("Selected", true);
    }

    public void Deselect() // Se llama cuando este botón es deseleccionado, ya sea a causa del cursor (ha salido) o con input de teclado/mando
    {
        if (animator != null)
            animator.SetBool("Selected", false);
    }

    //POINTER ENTER/EXIT // Quitar los dos metodos siguientes en el caso de que se quiera un menu solo controlado por input de teclado/mando
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name);
        //Debug.Log("MainMenuScript = " + MainMenuScript);
        //Debug.Log("pauseMenuScript = " + pauseMenuScript);
        //Debug.Log("levelSelectorMenu = " + levelSelectorMenu);

        if (MainMenuScript != null)
        {
            if (MainMenuScript.cameraTransform.position == new Vector3(0, 0, -10))
                MainMenuScript.ButtonChange(thisButtonIndex);
        }

        else if (pauseMenuScript != null) pauseMenuScript.ButtonChange(thisButtonIndex);

        else if (levelSelectorMenu != null) levelSelectorMenu.ButtonChange(thisButtonIndex);

        onPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointer = false;
        //animator.SetBool("Selected", false); // Deseleccionar el boton graficamente cuando el cursor salga
    }
}
