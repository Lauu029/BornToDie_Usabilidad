using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerConnected : MonoBehaviour
{

    float X_lastValue;
    float X_currentValue;

    float Y_lastValue;
    float Y_currentValue;

    Vector2 lastMousePos;
    Vector2 currentMousePos;

    GameObject image;
    [SerializeField]
    Text text;

    public bool usingController;

    private void Awake()
    {
        X_lastValue = 0;
        Y_lastValue = 0;
        image = GetComponentInChildren<Image>().gameObject;
        image.gameObject.SetActive(false);

        Cursor.visible = false;
    }

    private void Update()
    {
        // BUTTON INPUT

        // Actualizar valores
        X_currentValue = Input.GetAxisRaw("Horizontal");
        Y_currentValue = Input.GetAxisRaw("Vertical");

        // Comprobar si se ha cambiado de control
        CheckAxis(ref X_lastValue, ref X_currentValue);
        CheckAxis(ref Y_lastValue, ref Y_currentValue);

        // Actualizar valores
        X_lastValue = Input.GetAxisRaw("Horizontal");
        Y_lastValue = Input.GetAxisRaw("Vertical");



        // MOUSE
        currentMousePos = Input.mousePosition;

        // CkeckMouse
        if (currentMousePos != lastMousePos)
            usingController = false;

        lastMousePos = Input.mousePosition;



        // Activar/Desactivar imagen
        image.gameObject.SetActive(usingController);

        if (usingController)
        {
            text.text = "Using Controller";
            text.color = Color.cyan;
            Cursor.visible = false;
        }
        else  
        {
            text.text = "Not using Controller";
            text.color = Color.red;
            Cursor.visible = true;
        }
    }


    void CheckAxis(ref float lastValue, ref float currentValue)
    {
        if (lastValue == 0 && (currentValue == 1 || currentValue == -1)) // Si se ha realizad un cambio muy brusco (solo posible en "Keyboard")
            usingController = false;
        else if (currentValue != 0 && currentValue != 1 && currentValue != -1)
            usingController = true;
    }
}
