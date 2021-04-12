using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ASIGNACION (todo lo que se tenga que referenciar con "[SerialisedField]")
    [SerializeField]
    bool isMusicSlider;

    // CONFIGURACION (todos los componentes de otros objetos que no necesiten "[SerialisedField]")
    PauseMenuScript pauseMenu;
    Animator buttonAnimator;
    [HideInInspector]
    public Slider slider;
    Text text;

    // VARIABLES
    [HideInInspector]
    public int thisButtonIndex; // Almacena el Index de este boton, asignado por el "ButtonManager" al principio en el Awake
    bool onPointer;
    bool buttonSelected;

    void Awake()
    {
        // ASIGNAR VARIABLES
        buttonAnimator = GetComponent<Animator>();
        pauseMenu = GetComponentInParent<PauseMenuScript>();
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<Text>();

        // VALORES INICIALES

    }

    void Update()
    {
        // Presionar un boton si se hace click sobre el 
        if (onPointer && Input.GetMouseButtonDown(0))
            pauseMenu.ButtonChange(thisButtonIndex);
    }

    public void UpdateSliderOnPause()
    {
        if (isMusicSlider) OnChangeValue(PlayerPrefs.GetFloat("MusicVolume", 5)); // Slider de musica
        else OnChangeValue(PlayerPrefs.GetFloat("SoundVolume", 5)); // Slider de sonido
    }

    public void OnChangeValue(float newValue)
    {
        FindObjectOfType<AudioManager>().AudioVolume(newValue, isMusicSlider); // Cambiar volumen en el "AudioManager"

        FindObjectOfType<AudioManager>().Play("Button", 1); // Sonido
        slider.value = newValue; // Actualizar slider

        // Cambiar texto
        if (newValue != 10) // Si el nuevo valor es menor que 10
        {
            if (isMusicSlider) text.text = "MUSIC   " + newValue; // Si es el slider del Sonido
            else text.text = "SOUND   " + newValue; // Si es el slider de la musica
        } else
        {
            if (isMusicSlider) text.text = "MUSIC  " + 10; // Si es el slider del Sonido 
            else text.text = "SOUND  " + 10; // Si es el slider de la musica
        }
    }

    public void Select() // Se llama cuando este botón es seleccionado, ya sea a causa del cursor (ha entrado) o con input de teclado/mando
    {
        buttonAnimator.SetBool("Selected", true);
        //GetComponent<Animator>().SetBool("Selected", true);
    }

    public void Deselect() // Se llama cuando este botón es deseleccionado, ya sea a causa del cursor (ha salido) o con input de teclado/mando
    {
        buttonAnimator.SetBool("Selected", false);
    }

    //POINTER ENTER/EXIT
    public void OnPointerEnter(PointerEventData eventData)
    {
        pauseMenu.ButtonChange(thisButtonIndex);
        onPointer = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointer = false;
        //animator.SetBool("Selected", false); // Deseleccionar el boton graficamente cuando el cursor salga
    }
}
