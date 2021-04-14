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
    [SerializeField]
    float distanceBetweenImages;

    enum TypeOfMinion { small, trampoline, explosion, fly }

    [Header("CONFIGURACION")]
    [SerializeField]
    Transform chicken;
    [SerializeField]
    GameObject[] allMinions;
    [SerializeField]
    Sprite[] minionsSprites;
    [SerializeField]
    Transform minionsImagePrefab;
    Animator animator;
    Transform spawner;
    ButtonsManager buttonManager;
    Transform minionsLeftUI;
    Transform nextMinionImagePoint; // A partir de este punto spawnean las imagenes
    Vector3 currentPosMinionsLeftUI; // Posicion en la que deberia estar el "minionImagesContainer"
    [SerializeField]
    Transform minionImagesCanvas;
    [SerializeField]
    Transform minionImagesContainer;
    [SerializeField]
    Text buttonText;


    // VARIABLES
    bool startedMoving; // Indica si el minion se ha empezado a mover

    bool onPointer;

    private void Awake()
    {
        // Configuracion
        animator = GetComponent<Animator>();
        spawner = GameObject.Find("Spawner").transform;
        buttonManager = GetComponentInParent<ButtonsManager>();
        minionsLeftUI = GameObject.Find("MinionsLeftUI").transform;
        nextMinionImagePoint = minionsLeftUI.GetChild(0);
        currentPosMinionsLeftUI = minionImagesContainer.position;
        buttonText.text = "NACER";

        UpdateMinionLeftUI();

        // Valores iniciales
    }

    private void Update()
    {
        // Comprobar si se hace click sobre el boton, y en tal caso, realizar la accion
        CheckClick();

        if (minionImagesContainer.position.y - Time.deltaTime < currentPosMinionsLeftUI.y) minionImagesContainer.position = new Vector3(minionImagesContainer.position.x, currentPosMinionsLeftUI.y);
        else minionImagesContainer.position = new Vector2(minionImagesContainer.position.x, minionImagesContainer.position.y - Time.deltaTime * 350);

        //if (minionsLeftUI.position == currentPosMinionsLeftUI)
        //{
        //}

        Debug.Log("minionImagesContainer = " + minionImagesContainer.position);
        Debug.Log("currentPosMinionsLeftUI = " + currentPosMinionsLeftUI);
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
                    else if (buttonManager.controllingMinion.GetComponent<PolloVolador>() != null)
                    {
                        buttonManager.controllingMinion.GetComponent<PolloVolador>().enabled = false;
                        Destroy(buttonManager.controllingMinion.GetComponent<Rigidbody2D>());
                    }
                }

                // Decirle al ButtonManager que se controla al nuevo minion
                buttonManager.controllingMinion = Instantiate(allMinions[minionIndex], spawner);

                // Bajar imagenes de los minions
                currentPosMinionsLeftUI = new Vector3(transform.position.x, currentPosMinionsLeftUI.y - distanceBetweenImages, 0);

                ordenMinionIndex++;

                // Si ya no hay mas minions
                if (ordenMinionIndex == ordenOfminions.Length) buttonText.text = "GOOO";

            } else if (ordenMinionIndex == ordenOfminions.Length)
            {
                // Eliminar el movimiento del minion activo
                if (buttonManager.controllingMinion != null)
                {
                    if (buttonManager.controllingMinion.GetComponent<BasicMovement>() != null)
                        buttonManager.controllingMinion.GetComponent<BasicMovement>().enabled = false;
                    else if (buttonManager.controllingMinion.GetComponent<PolloVolador>() != null)
                    {
                        buttonManager.controllingMinion.GetComponent<PolloVolador>().enabled = false;
                        Destroy(buttonManager.controllingMinion.GetComponent<Rigidbody2D>());
                    }
                }

                // Si no hay minions, soltar a la "Gallina"
                Instantiate(chicken.gameObject, spawner);

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
        //for (int i = ordenMinionIndex; i < imagesContainer.childCount; i++)
        //    Destroy(nextMinionImagePoint.GetChild(i));

        for (int i = 0; i < ordenOfminions.Length; i++) // Crear una imagen por cada minion
        {
            Transform newMinionImage = Instantiate(minionsImagePrefab, nextMinionImagePoint.position + new Vector3(0, i* distanceBetweenImages, 0), transform.rotation, minionImagesCanvas);

            newMinionImage.SetParent(minionImagesContainer); // Hacerlo hijo del contenedor de imagenes, para despues mover unicamente ese obj

            // Cambiar Sprite
            newMinionImage.GetChild(newMinionImage.childCount - 1).GetComponent<Image>().sprite = minionsSprites[NameToInt(ordenOfminions[i])];
            // Cambiar textos del script "ShowText"
            newMinionImage.GetComponent<ShowText>().infoTitle = titlesMinions[NameToInt(ordenOfminions[i])];
            newMinionImage.GetComponent<ShowText>().infoContent = contentsMinions[NameToInt(ordenOfminions[i])];
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ordenMinionIndex != ordenOfminions.Length + 1) // Si queda algun Minion por spawnear
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
