using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SigMinion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("VALORES MODIFICABLES")]
    [SerializeField]
    TypeOfMinion[] ordenOfminions;
    int ordenMinionIndex; // Recorre el array de minions
    [SerializeField]
    string[] titlesMinions;
    [SerializeField]
    string[] contentsMinions;


    enum TypeOfMinion { small, trampoline, explosion, fly }

    [Header("CONFIGURACION")]
    [SerializeField]
    GameObject[] allMinions;
    [SerializeField]
    Sprite[] minionsSprites;
    [SerializeField]
    Transform minionsImagePrefab;
    [SerializeField]
    Transform imagesContainer;
    Animator animator;
    Transform spawner;
    ButtonsManager buttonManager;
    Transform nextMinionImagePoint;

    // VARIABLES
    bool startedMoving; // Indica si el minion se ha empezado a mover

    bool onPointer;

    private void Awake()
    {
        // Configuracion
        animator = GetComponent<Animator>();
        spawner = GameObject.Find("Spawner").transform;
        buttonManager = GetComponentInParent<ButtonsManager>();
        nextMinionImagePoint = GameObject.Find("NextMinionImagePoint").transform;

        UpdateMinionLeftUI();

        // Valores iniciales
    }

    private void Update()
    {
        // Comprobar si se hace click sobre el boton, y en tal caso, realizar la accion
        CheckClick();
    }

    public void CheckClick()
    {
        if (onPointer && Input.GetMouseButtonDown(0))
        {
            if (ordenMinionIndex != ordenOfminions.Length) // Si todavia quedan minions
            {
                int minionIndex = NameToInt(ordenOfminions[ordenMinionIndex]);
                Debug.Log("minionIndex = " + minionIndex);

                // Eliminar el movimiento del minion activo
                if (buttonManager.controllingMinion != null)
                {
                    if (buttonManager.controllingMinion.GetComponent<BasicMovement>() != null)
                        buttonManager.controllingMinion.GetComponent<BasicMovement>().enabled = false;
                }
                //else if () // Script Volador de LAURA

                // Decirle al ButtonManager que se controla al nuevo minion
                buttonManager.controllingMinion = Instantiate(allMinions[minionIndex], spawner);

                ordenMinionIndex++;
            }
        }
    }

    int NameToInt(TypeOfMinion typeOfMinion) // Pasa del enum "TypeOfMinion" a su representacion fisica en el array "ordenOfminions"
    {
        int minionIndex = -1;

        switch (typeOfMinion)
        {
            case TypeOfMinion.small:
                minionIndex = 0;
                break;
            case TypeOfMinion.trampoline:
                minionIndex = 1;
                break;
            case TypeOfMinion.explosion:
                minionIndex = 2;
                break;
            case TypeOfMinion.fly:
                minionIndex = 3;
                break;
        }

        return minionIndex;
    }

    public void UpdateMinionLeftUI()
    {
        for (int i = ordenMinionIndex; i < imagesContainer.childCount; i++)
            Destroy(nextMinionImagePoint.GetChild(i));

        for (int i = 0; i < ordenOfminions.Length; i++) // Crear una imagen por cada minion
        {
            Transform newMinionImage = Instantiate(minionsImagePrefab, nextMinionImagePoint.position + new Vector3(0, i* 200, 0), transform.rotation, imagesContainer);

            // Cambiar Sprite
            newMinionImage.GetChild(newMinionImage.childCount - 1).GetComponent<Image>().sprite = minionsSprites[NameToInt(ordenOfminions[i])];
            // Cambiar textos del script "ShowText"
            newMinionImage.GetComponent<ShowText>().infoTitle = titlesMinions[NameToInt(ordenOfminions[i])];
            newMinionImage.GetComponent<ShowText>().infoContent = contentsMinions[NameToInt(ordenOfminions[i])];
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ordenMinionIndex != ordenOfminions.Length) // Si queda algun Minion por spawnear
        {
            onPointer = true;
            animator.SetBool("OnPointer", true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointer = false;
        animator.SetBool("OnPointer", false);
    }
}
