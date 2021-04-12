using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // ASIGNACION (todo lo que se tenga que referenciar con "[SerialisedField]")
    [SerializeField]
    Transform buttonGroup; // GameObject padre de todos los botones (unicamente los botones)

    // CONFIGURACION (todos los componentes de otros objetos que no necesiten "[SerialisedField]")
    ButtonScript[] allButtons;
    [HideInInspector]
    public Transform cameraTransform;
    Animator cameraAnimator;

    // VARIABLES
    int buttonIndex; // Numero del boton que esta seleccionado en este momento
    IEnumerator currentCoroutine; // Contiene la corrutina que se esta ejecutando en este momento

    private void Start() // Start en vez de Awake para que al boton le de tiempo a asignar sus variables antes de que se utilizen en este metodo
    {
        // ASIGNAR VARIABLES
        cameraTransform = FindObjectOfType<Camera>().transform;
        cameraAnimator = cameraTransform.GetComponent<Animator>();

        // VALORES INICIALES
        buttonIndex = 0;

        // Meter en el array "allButtons" todos los hijos de "buttonGroup"
        allButtons = new ButtonScript[buttonGroup.childCount];
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = buttonGroup.GetChild(i).GetComponent<ButtonScript>();
            allButtons[i].thisButtonIndex = i;
        }

        // Seleccionar por defecto el primer boton
        allButtons[0].Select();
    }
    private void Update()
    {
        // Comprobar si se ha seleccionado un nuevo boton
        if (currentCoroutine == null) // Si no se esta ejecutando ninguna corrutina, empezar una nueva
            StartCoroutine(CheckButtonAxisChange()); // Borrar si el menu solo se va a controlar con el raton

        // Comprobar si se ha elegido el boton actual
        CheckButtonPressed();

        // DEBUG
        //Debug.Log("buttonIndex = " + buttonIndex);
        //Debug.Log("Input.GetAxisRaw() = " + Input.GetAxisRaw("Vertical"));
    }

    IEnumerator CheckButtonAxisChange() // Comprueba si se ha intentado cambiar el boton actual mediante input (Teclado/Mando), (Arriba/Abajo)
    {
        if (cameraTransform.position == new Vector3(0, 0, -10) && Input.GetAxisRaw("Vertical") != 0) // Solo si se ha introducido el input correcto (Arriba/Abajo) y la camara esta apuntando al centro
        {
            FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido

            allButtons[buttonIndex].Deselect();// Deseleccionar el antiguo boton
            if (Input.GetAxisRaw("Vertical") < 0) // Abajo
            {
                // Modificar buttonIndex
                if (buttonIndex + 1 == allButtons.Length)
                    buttonIndex = 0;
                else buttonIndex++;

            } else // Arriba
            {
                // Modificar buttonIndex
                if (buttonIndex - 1 == -1)
                    buttonIndex = allButtons.Length - 1;
                else buttonIndex--;
            }
            allButtons[buttonIndex].Select(); // Seleccionar el nuevo boton

            // Actualiza el valor de "currentCoroutine" para indicar que se esta ejecutando esta corrutina y lo libera cuando termina
            currentCoroutine = CheckButtonAxisChange();
            yield return new WaitForSeconds(0.2f);
            currentCoroutine = null;
        }
    }
    public void ButtonChange(int buttonIndexRef) // Cambia el boton activo al pasado por referencia "buttonIndex", se llama al entrar el "Pointer" en un boton
    {
        if (buttonIndex != buttonIndexRef) // Ejecutar solo si se selecciona un boton diferente al seleccionado actualmente
        {
            FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido

            allButtons[buttonIndex].Deselect();// Deseleccionar el antiguo boton
            buttonIndex = buttonIndexRef; // Asignar el nuevo boton
            allButtons[buttonIndexRef].Select();// Seleccionar el nuevo boton
        }
    }

    void CheckButtonPressed()
    {
        if (Input.GetButtonDown("Action"))
        {
            PressButton(buttonIndex);
        }

        // Volver a poner la camara en el centro si se presiona cualquien boton al estar en uno de los lados
        if (Input.anyKeyDown)
        {
            // CheckLeft
            if (cameraTransform.position.x == -20)
            {
                cameraAnimator.SetBool("Left", false); 
                FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido
            }

            // CheckRight
            if (cameraTransform.position.x == 20)
            {
                cameraAnimator.SetBool("Right", false); 
                FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido
            }
        }
    }

    public void PressButton(int buttonIndexRef) // Presiona el boton con el index indicado
    {
        FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido

        if (cameraTransform.position == new Vector3(0, 0, -10)) // Si la camara esta en el centro
            switch (buttonIndexRef)
            {
                case 0:
                    PlaySelect();
                    break;
                case 1:
                    SettingsSelect();
                    break;
                case 2:
                    ControlsSelect();
                    break;
                case 3:
                    CreditsSelect();
                    break;
                case 4:
                    QuitSelect();
                    break;
            }
    }

    // BUTTON ACTIONS
    public void PlaySelect()
    {
        Debug.Log("Gameplay");
        GameManager.GetInstance().ChangeScene("Gameplay");
    }
    public void SettingsSelect()
    {
        FindObjectOfType<PauseMenuScript>().SelectPauseMenu();
        cameraAnimator.SetBool("Down", true);
        Debug.Log("SettingsSelect");
    }
    public void ControlsSelect()
    {
        cameraAnimator.SetBool("Left", true);
        Debug.Log("ControlsSelect");
    }
    public void CreditsSelect()
    {
        cameraAnimator.SetBool("Right", true);
        Debug.Log("CreditsSelect");
    }
    public void QuitSelect()
    {
        Application.Quit();
        Debug.Log("QuitSelect");
    }
}
