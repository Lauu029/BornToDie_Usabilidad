using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickBorn : TrackerEvent
{
    public int CurrentLevel { get; set; }
    public ClickBorn()
    {
        type = eventType.ClickBornEvent;
    }
}
