using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectorMenu : MonoBehaviour
{
    // ASIGNACION (todo lo que se tenga que referenciar con "[SerialisedField]")
    [SerializeField]
    Transform buttonGroup; // GameObject padre de todos los botones (unicamente los botones)

    [SerializeField]
    Transform locker;

    // CONFIGURACION (todos los componentes de otros objetos que no necesiten "[SerialisedField]")
    Transform[] allButtons;
    Transform obj; // Objeto que se activa y desactiva con todos los elementos dentro

    // VARIABLES
    int buttonIndex; // Numero del boton que esta seleccionado en este momento
    bool paused;
    IEnumerator currentCoroutine; // Contiene la corrutina que se esta ejecutando en este momento
    Transform cameraTransform;

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

        // CHANGE
        obj.gameObject.SetActive(true);


        // DIBUJAR CANDADOS

        // Recorre desde el ultimo nivel desbloqueado + 1 hasta el penultimo boton (no incluye el boton "Go Back") 
        for (int i = GameManager.GetInstance().currentLevel + 1; i < allButtons.Length - 1; i++) 
        {
            Transform canvas = FindObjectOfType<Canvas>().transform;
            Vector3 instantiatePosition = new Vector3(allButtons[i].position.x + 2, allButtons[i].position.y); // posicion del candado
            GameObject newGameObject = Instantiate(locker.gameObject, instantiatePosition, Quaternion.identity, canvas);

            // Cambiar color del boton
            allButtons[i].GetComponentInChildren<Text>().color = Color.red;
        }
    }

    private void Update()
    {
        // CHANGE
        if (cameraTransform.position.y == 12)
        {
            // Comprobar si se ha seleccionado un nuevo boton
            if (currentCoroutine == null) // Si no se esta ejecutando ninguna corrutina, empezar una nueva
            {
                StartCoroutine(CheckButtonAxisChange()); // Borrar si el menu solo se va a controlar con el raton
            }

            // Comprobar si se ha elegido el boton actual
            CheckButtonPressed();
        }

        //Debug.Log("buttonIndex = " + buttonIndex);

        // DEBUG
        //Debug.Log("buttonIndex = " + buttonIndex);
        //Debug.Log("currentCoroutine = " + currentCoroutine);
        //Debug.Log("Input.GetAxisRaw() = " + Input.GetAxisRaw("Vertical"));
    }
    public void SelectPauseMenu()
    {
        //Deselect(allButtons[allButtons.Length - 1]); // Deseleccionar el ultimo boton // BUG RARO
        Select(allButtons[allButtons.Length - 1]); // Seleccionar por defecto el ultimo boton
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
                int newButtonIndex = buttonIndex + 1;

                if (newButtonIndex == allButtons.Length) buttonIndex = 0;

                else if (newButtonIndex > GameManager.GetInstance().currentLevel) buttonIndex = allButtons.Length - 1;

                else buttonIndex = newButtonIndex;
            }
            else // Arriba
            {
                int newButtonIndex = buttonIndex - 1;

                if (newButtonIndex == -1) buttonIndex = allButtons.Length - 1;

                else if (newButtonIndex > GameManager.GetInstance().currentLevel) buttonIndex = GameManager.GetInstance().currentLevel;

                else buttonIndex = newButtonIndex;
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
                }
                else // Arriba
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
        Debug.Log("LevelSelector");

        if (buttonIndexRef == allButtons.Length - 1 || buttonIndexRef <= GameManager.GetInstance().currentLevel) // Si es un boton seleccionable
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
        if (cameraTransform.position.y == -20)
            FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido

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
        if (buttonIndexRef == allButtons.Length - 1 || buttonIndexRef <= GameManager.GetInstance().currentLevel)
        {
            FindObjectOfType<AudioManager>().Play("Button", 1);
            if (buttonIndexRef == allButtons.Length - 1) // Si es el GoBack
            {
                GoBackSelect();
            }
            else
            {
                GameManager.GetInstance().ChangeLevel(buttonIndexRef);
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
    public void GoBackSelect()
    {
        cameraTransform.GetComponent<Animator>().SetBool("Up", false);

        Debug.Log("GoBackSelect");
    }
}
