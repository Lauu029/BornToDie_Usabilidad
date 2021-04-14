using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainBowEffect : MonoBehaviour
{
    Image thisImage;
    Text thisText;

    float R = 0;
    float G = 0;
    float B = 1;


    private void Awake()
    {
        if (GetComponent<Image>() != null) thisImage = GetComponent<Image>();
        else if (GetComponent<Text>() != null) thisText = GetComponent<Text>();
    }


    private void Update()
    {
        Color newColor;

        Add(ref G);

        newColor = new Color ( Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        if (thisImage != null) thisImage.color = newColor;
        else if (thisText != null) thisText.color = newColor;
    }

    void Add(ref float value)
    {
        if (value + Time.deltaTime >= 1) value = 1;
        else value += Time.deltaTime;
    }

    void Substract(ref float value)
    {
        if (value - Time.deltaTime <= 0) value = 0;
        value -= Time.deltaTime;
    }
}
