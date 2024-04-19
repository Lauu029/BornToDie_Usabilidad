using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

public class ProgressionEvent : TrackerEvent
{
    
    int dinero = 10;
    
    public int CurrentDinero
{
        get { return dinero; }
        set { dinero = value; }
    }
    public ProgressionEvent()
    {
        type = eventType.ProgressionEvent;
        //CurrentDinero = 20;
    }

}
