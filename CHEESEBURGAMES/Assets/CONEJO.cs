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
        ClickSpawnedRabbitEvent even = new ClickSpawnedRabbitEvent();

        CSVSerialize cSV = new CSVSerialize();
        string progresion = cSV.serialize(prog);
        string rabbit= cSV.serialize(even);

        Debug.Log(cSV.InitFileFormat());
        Debug.Log("-------");
        Debug.Log(progresion);
        Debug.Log("-------");
        Debug.Log(rabbit);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
