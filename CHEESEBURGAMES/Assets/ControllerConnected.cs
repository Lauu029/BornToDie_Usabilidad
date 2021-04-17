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

    Transform mouseCursorObj;

    public bool usingController;

    private void Awake()
    {
        X_lastValue = 0;
        Y_lastValue = 0;
        if (GetComponentInChildren<Image>() != null)
        {
            image = GetComponentInChildren<Image>().gameObject;
            image.gameObject.SetActive(false);
        }

        mouseCursorObj = FindObjectOfType<MouseCursor>().transform.GetChild(0);

        currentMousePos = Input.mousePosition;
        lastMousePos = Input.mousePosition;
    }

    private void Start()
    {
        usingController = GameManager.GetInstance().usingController;
    }

    private void Update()
    {
        Debug.Log("usingController = " + usingController);

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
        if (image != null)
            image.gameObject.SetActive(usingController);

        if (usingController)
        {
            text.text = "Controller Connected";
            text.color = Color.cyan;
            mouseCursorObj.gameObject.SetActive(false);
        }
        else
        {
            text.text = "Controller Disconnected";
            text.color = Color.red;
            mouseCursorObj.gameObject.SetActive(true);
        }
    }

    void CheckAxis(ref float lastValue, ref float currentValue)
    {
        if (lastValue == 0 && (currentValue == 1 || currentValue == -1)) // Si se ha realizad un cambio muy brusco (solo posible en "Keyboard")
        {
            GameManager.GetInstance().usingController = usingController;

            usingController = false;
        }
        else if (currentValue != 0 && currentValue != 1 && currentValue != -1)
        {
            GameManager.GetInstance().usingController = usingController;

            usingController = true;
        }
    }

   public void SetUpUsingController(bool usingControllerBool)
    {
        usingController = usingControllerBool;
    }
}
