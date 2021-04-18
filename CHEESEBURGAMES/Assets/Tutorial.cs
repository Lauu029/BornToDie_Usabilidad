using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    GameObject firstText;
    GameObject secondText;

    private void Awake()
    {
        firstText = transform.GetChild(0).gameObject;
        secondText = transform.GetChild(1).gameObject;

        parte = parteDelTutorial.spawnRabbit;
    }

    public enum parteDelTutorial { spawnRabbit, move, go, getToCarrot}

    public parteDelTutorial parte;

    Trampoline_Minion trampolineBoy;

    private void Update()
    {

        if (parte == parteDelTutorial.spawnRabbit)
        {
            firstText.GetComponentInChildren<Animator>().SetBool("Selected", true);
            secondText.GetComponentInChildren<Animator>().SetBool("Selected", false);
            firstText.GetComponentInChildren<Text>().text = "<- Spawn babyRabbit";

            if (FindObjectOfType<Trampoline_Minion>() != null)
            {
                trampolineBoy = FindObjectOfType<Trampoline_Minion>();
                parte = parteDelTutorial.move;
            }
        }
        else if (parte == parteDelTutorial.move)
        {
            firstText.GetComponentInChildren<Animator>().SetBool("Selected", false);
            secondText.GetComponentInChildren<Animator>().SetBool("Selected", true);
            secondText.GetComponentInChildren<Text>().text = "Move babyRabbit                                                 <- WASD/LeftJoystick";

            if (trampolineBoy != null && trampolineBoy.transform.position.y < -2.5f)
            {
                parte = parteDelTutorial.go;
            }
        }
        else if (parte == parteDelTutorial.go)
        {
            firstText.GetComponentInChildren<Animator>().SetBool("Selected", true);
            secondText.GetComponentInChildren<Animator>().SetBool("Selected", false);
            firstText.GetComponentInChildren<Text>().text = "<- Make mamaRabbit walk";

            if (trampolineBoy != null && trampolineBoy.transform.position.y > -2.5f) parte = parteDelTutorial.move;

            if (!trampolineBoy.gameObject.GetComponent<BasicMovement>().enabled) parte = parteDelTutorial.getToCarrot;
        }
        else if (parte == parteDelTutorial.getToCarrot)
        {
            firstText.GetComponentInChildren<Animator>().SetBool("Selected", false);
            secondText.GetComponentInChildren<Animator>().SetBool("Selected", true);
            secondText.GetComponentInChildren<Text>().color = new Color(1, 0.1949536f, 0);
            secondText.GetComponentInChildren<Text>().text = "Get to the carrot                              at any price >:) ->";
        }

    }


}
