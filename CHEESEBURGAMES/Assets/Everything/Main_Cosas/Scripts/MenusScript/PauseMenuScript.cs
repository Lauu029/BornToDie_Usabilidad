using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // ASIGNACION (todo lo que se tenga que referenciar con "[SerialisedField]")
    [SerializeField]
    Transform buttonGroup; // GameObject padre de todos los botones (unicamente los botones)

    // CONFIGURACION (todos los componentes de otros objetos que no necesiten "[SerialisedField]")
    Transform[] allButtons;
    Transform obj; // Objeto que se activa y desactiva con todos los elementos dentro

    // VARIABLES
    int buttonIndex; // Numero del boton que esta seleccionado en este momento
    bool paused;
    IEnumerator currentCoroutine; // Contiene la corrutina que se esta ejecutando en este momento
    typeOfPauseMenu thisType; // Define el comportamiento de este menu dependiendo de si esta en el gameplay o es parte de otro menu
    Transform cameraTransform;
    enum typeOfPauseMenu{inGame, inMainMenu }; 

    private void Start() // Start en vez de Awake para que al boton le de tiempo a asignar sus variables antes de que se utilizen en este metodo
    {
        // ASIGNAR VARIABLES
        obj = transform.GetChild(0);
        cameraTransform = FindObjectOfType<Camera>().transform;

        // VALORES INICIALES
        buttonIndex = 0;

        // Meter en el array "allButtons" todos los hijos de "buttonGroup"
        allButtons = new Transform[buttonGroup.childCount];
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = buttonGroup.GetChild(i);

            // Comprobar si es un "Boton" o un "Slider"
            if (allButtons[i].GetComponent<ButtonScript>() != null) // Es un boton
                allButtons[i].GetComponent<ButtonScript>().thisButtonIndex = i;
            else // Es un slider
                allButtons[i].GetComponent<SliderScript>().thisButtonIndex = i;
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            thisType = typeOfPauseMenu.inMainMenu;
            obj.gameObject.SetActive(true);
            // Asignar la camara si se esta en el menu principal
        } else
        {
            obj.gameObject.SetActive(false);
            thisType = typeOfPauseMenu.inGame;
        }
    }

    private void Update()
    {
        if (thisType == typeOfPauseMenu.inGame) // Es del juego
        {
            if (Input.GetButtonDown("Pause")) // Pausar/Despausar
            {
                if (paused) ResumeSelect();
                else PauseSelect();
            }

            if (paused)
            {
                // Comprobar si se ha seleccionado un nuevo boton
                if (currentCoroutine == null) // Si no se esta ejecutando ninguna corrutina, empezar una nueva
                {
                    StartCoroutine(CheckButtonAxisChange()); // Borrar si el menu solo se va a controlar con el raton
                }

                // Comprobar si se ha elegido el boton actual
                CheckButtonPressed();
            }

        } else if (thisType == typeOfPauseMenu.inMainMenu) // Es parte de otro menu
        {
            if (cameraTransform.position.y == -12)
            {
                // Comprobar si se ha seleccionado un nuevo boton
                if (currentCoroutine == null) // Si no se esta ejecutando ninguna corrutina, empezar una nueva
                {
                    StartCoroutine(CheckButtonAxisChange()); // Borrar si el menu solo se va a controlar con el raton
                }

                // Comprobar si se ha elegido el boton actual
                CheckButtonPressed();
            }
        }

        // DEBUG
        //Debug.Log("buttonIndex = " + buttonIndex);
        //Debug.Log("currentCoroutine = " + currentCoroutine);
        //Debug.Log("Input.GetAxisRaw() = " + Input.GetAxisRaw("Vertical"));
    }
    public void SelectPauseMenu()
    {
        Select(allButtons[0]); // Seleccionar por defecto el primer boton
        for (int i = 0; i < allButtons.Length; i++) // Actualizar los sliders
            if (allButtons[i].GetComponent<SliderScript>()) allButtons[i].GetComponent<SliderScript>().UpdateSliderOnPause();
    }

    IEnumerator CheckButtonAxisChange() // Comprueba si se ha intentado cambiar el boton actual mediante input (Teclado/Mando), (Arriba/Abajo)
    {
        if (Input.GetAxisRaw("Vertical") != 0) // Solo si se ha introducido el input correcto (Arriba/Abajo) y la camara esta apuntando al centro
        {
            Deselect(allButtons[buttonIndex]); // Deseleccionar el antiguo boton
            if (Input.GetAxisRaw("Vertical") < 0) // Abajo
            {
                // Modificar buttonIndex
                if (buttonIndex + 1 == allButtons.Length)
                    buttonIndex = 0;
                else buttonIndex++;
            }
            else // Arriba
            {
                // Modificar buttonIndex
                if (buttonIndex - 1 == -1)
                    buttonIndex = allButtons.Length - 1;
                else buttonIndex--;
            }
            Select(allButtons[buttonIndex]); // Seleccionar el nuevo boton

            // Actualiza el valor de "currentCoroutine" para indicar que se esta ejecutando esta corrutina y lo libera cuando termina
            currentCoroutine = CheckButtonAxisChange();
            yield return new WaitForSecondsRealtime(0.2f);
            currentCoroutine = null;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            SliderScript selectedSlider = allButtons[buttonIndex].GetComponent<SliderScript>();
            if (selectedSlider != null) // Si es un slider cambiarle el valor
            {
                float newValue = selectedSlider.slider.value;

                if (Input.GetAxisRaw("Horizontal") < 0) // Abajo
                {
                    // Modificar newValue
                    if (newValue - 1 == -1)
                        newValue = selectedSlider.slider.maxValue;
                    else newValue--;
                } else // Arriba
                {
                    // Modificar newValue
                    if (newValue + 1 > selectedSlider.slider.maxValue)
                        newValue = 0;
                    else newValue++;
                }
                allButtons[buttonIndex].GetComponent<SliderScript>().OnChangeValue(newValue); // Modificar slider con "newValue"
            }
            // Actualiza el valor de "currentCoroutine" para indicar que se esta ejecutando esta corrutina y lo libera cuando termina
            currentCoroutine = CheckButtonAxisChange();
            yield return new WaitForSecondsRealtime(0.2f);
            currentCoroutine = null;
        }
    }
    public void ButtonChange(int buttonIndexRef) // Cambia el boton activo al pasado por referencia "buttonIndex", se llama al entrar el "Pointer" en un boton
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" && paused)
        {
            if (buttonIndex != buttonIndexRef) // Ejecutar solo si se selecciona un boton diferente al seleccionado actualmente
            {
                Deselect(allButtons[buttonIndex]);// Deseleccionar el antiguo boton
                buttonIndex = buttonIndexRef; // Asignar el nuevo boton
                Select(allButtons[buttonIndexRef]);// Seleccionar el nuevo boton
            }
        } else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (buttonIndex != buttonIndexRef) // Ejecutar solo si se selecciona un boton diferente al seleccionado actualmente
            {
                Deselect(allButtons[buttonIndex]);// Deseleccionar el antiguo boton
                buttonIndex = buttonIndexRef; // Asignar el nuevo boton
                Select(allButtons[buttonIndexRef]);// Seleccionar el nuevo boton
            }
        }
    }

    void Select(Transform gameObject)
    {
        if (thisType == typeOfPauseMenu.inGame)
        {
            if (paused)
                FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido
        } else if (thisType == typeOfPauseMenu.inMainMenu)
        {
            if (cameraTransform.position.y == -20)
                FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido
        }


        gameObject.gameObject.SetActive(true); // Activar el boton antes de llamarlo
        gameObject.GetComponent<Animator>().enabled = true;

        if (gameObject.GetComponent<ButtonScript>()) gameObject.GetComponent<ButtonScript>().Select();
        else if (gameObject.GetComponent<SliderScript>()) gameObject.GetComponent<SliderScript>().Select();
    }
    void Deselect(Transform gameObject)
    {
        if (gameObject.GetComponent<ButtonScript>()) gameObject.GetComponent<ButtonScript>().Deselect();
        else if (gameObject.GetComponent<SliderScript>()) gameObject.GetComponent<SliderScript>().Deselect();
    }

    void CheckButtonPressed()
    {
        if (Input.GetButtonDown("Action"))
        {
            PressButton(buttonIndex);
        }
    }

    public void PressButton(int buttonIndexRef) // Presiona el boton con el index indicado
    {
        if (thisType == typeOfPauseMenu.inGame)
        {
            if (paused)
                switch (buttonIndexRef)
                {
                    case 0:
                        ResumeSelect();
                        break;
                    case 1:
                        RestartSelect();
                        break;
                    case 2:
                        MainMenuSelect();
                        break;
                    case 4:
                        QuitSelect();
                        break;
                }

        } else if (thisType == typeOfPauseMenu.inMainMenu)
        {
            switch (buttonIndexRef)
            {
                case 0:
                    ResumeSelect();
                    break;
                case 3:
                    QuitSelect();
                    break;
            }
        }
    }

    public void PauseSelect()
    {
        obj.gameObject.SetActive(true); // Activar menu
        Select(allButtons[0]); // Seleccionar por defecto el primer boton
        buttonIndex = 0;
        paused = true;
        Time.timeScale = 0; // Parar el tiempo
        for (int i = 0; i < allButtons.Length; i++) // Actualizar los sliders
            if (allButtons[i].GetComponent<SliderScript>()) allButtons[i].GetComponent<SliderScript>().UpdateSliderOnPause();
    }

    // BUTTON ACTIONS
    public void ResumeSelect()
    {
        if (thisType == typeOfPauseMenu.inGame)
        {
            Debug.Log("ResumeSelect");
            obj.gameObject.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        } else if (thisType == typeOfPauseMenu.inMainMenu)
        {
            Debug.Log("ResumeSelect");
            cameraTransform.GetComponent<Animator>().SetBool("Down", false);
        }
    }
    public void MainMenuSelect()
    {
        Debug.Log("MainMenuSelect");
        Time.timeScale = 1;
        GameManager.GetInstance().ChangeScene("MainMenu");
    }
    public void ControlsSelect()
    {
        Debug.Log("ControlsSelect");
    }
    public void CreditsSelect()
    {
        Debug.Log("CreditsSelect");
    }
    public void QuitSelect()
    {
        Application.Quit();
        Debug.Log("QuitSelect");
    }

    public void RestartSelect()
    {
        GameManager.GetInstance().ChangeLevel(SceneManager.GetActiveScene().buildIndex);
    }
}

