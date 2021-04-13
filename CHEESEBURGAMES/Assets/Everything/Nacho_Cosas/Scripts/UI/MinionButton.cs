using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("VALORES MODIFICABLES")]
    [SerializeField]
    TypeOfMinion spawnMinionOfType;
    [SerializeField]
    int numMinionsMax;

    int numMinions; // numero de minions restantes

    enum TypeOfMinion { small, trampoline, explosion, fly}

    [Header("CONFIGURACION")]
    [SerializeField]
    GameObject[] allMinions;
    Animator animator;
    Transform spawner;
    ButtonsManager buttonManager;

    // VARIABLES

    bool onPointer;

    private void Awake()
    {
        // Configuracion
        animator = GetComponent<Animator>();
        spawner = GameObject.Find("Spawner").transform;
        buttonManager = GetComponentInParent<ButtonsManager>();

        // Valores iniciales
        numMinions = numMinionsMax;
    }

    private void Update()
    {
        // Si se hace click sobre el boton
        Click();
    }

    public void Click()
    {
        if (onPointer && Input.GetMouseButtonDown(0))
        {
            if (numMinions != 0) // Si todavia quedan minions
            {
                int minionIndex = -1;

                switch (spawnMinionOfType)
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

                // Eliminar el movimiento del minion activo
                if (buttonManager.controllingMinion != null)
                {
                    if (buttonManager.controllingMinion.GetComponent<BasicMovement>() != null)
                        buttonManager.controllingMinion.GetComponent<BasicMovement>().enabled = false;
                }
                //else if () // Script de LAURA
                
                // Decirle al ButtonManager que se controla al nuevo minion
                buttonManager.controllingMinion = Instantiate(allMinions[minionIndex], spawner);

                numMinions--;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointer = true;
        animator.SetBool("OnPointer", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointer = false;
        animator.SetBool("OnPointer", false);
    }
}
