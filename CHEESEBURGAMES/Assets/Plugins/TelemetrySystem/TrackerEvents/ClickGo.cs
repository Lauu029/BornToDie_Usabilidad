using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickGo : TrackerEvent
{
    public int CurrentLevel { get; set; }
    public ClickGo()
    {
        type = eventType.ClickGoEvent;
    }

}
