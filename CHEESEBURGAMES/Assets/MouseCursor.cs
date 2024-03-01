using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    Image cursor;
    double timer;
    Color cursorColor;
    private void Awake()
    {
        cursorColor = cursor.color;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cursorPos = Input.mousePosition;
        transform.position = cursorPos;
        if (Input.GetMouseButtonDown(0))
        {
            cursor.color = Color.black;
            timer = 0;
        }
        if (cursor.color == Color.black)
            timer += Time.deltaTime;
        if(timer>=0.15 && cursor.color == Color.black)
        {
            cursor.color = cursorColor;
            
        }
    }
}
