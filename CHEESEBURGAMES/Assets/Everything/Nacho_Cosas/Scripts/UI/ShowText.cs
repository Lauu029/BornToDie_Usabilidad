using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("VALORES MODIFICABLES")]
    public string infoTitle;
    public string infoContent;

    [Header("CONFIGURACION")]
    [SerializeField]
    Transform infoPanelPrefab;
    Transform currentInfoPanel;
    Camera camera;
    bool onPointer;

    private void Awake()
    {
        // Configuracion
        camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (onPointer)
        {
            currentInfoPanel.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Instanciar el panel con la info
        currentInfoPanel = Instantiate(infoPanelPrefab, transform.parent);

        // Modificar titulo del "infopanel"
        currentInfoPanel.transform.GetChild(1).GetComponent<Text>().text = infoTitle;
        // Modificar contenido del "infopanel"
        currentInfoPanel.transform.GetChild(2).GetComponent<Text>().text = infoContent;

        onPointer = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(currentInfoPanel.gameObject);

        onPointer = false;
    }
}
