using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

public class ProgressionEvent : TrackerEvent
{
    
    public ProgressionEvent()
    {
        Type = eventType.ProgressionEvent;
    }

}
