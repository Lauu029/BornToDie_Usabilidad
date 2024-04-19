using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONEJO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ProgressionEvent prog = new ProgressionEvent();
        prog.ToJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
