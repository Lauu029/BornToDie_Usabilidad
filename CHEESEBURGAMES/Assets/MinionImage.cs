using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinionImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    bool onPointer;

    Animator thisAnimator;

    private void Awake()
    {
        thisAnimator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        onPointer = true;
        thisAnimator.SetBool("OnPointer", onPointer);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointer = false;
        thisAnimator.SetBool("OnPointer", onPointer);
    }
}
