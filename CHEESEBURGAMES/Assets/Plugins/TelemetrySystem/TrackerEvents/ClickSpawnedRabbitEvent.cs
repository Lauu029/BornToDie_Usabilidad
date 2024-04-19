using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickSpawnedRabbitEvent : TrackerEvent
{
    public int CurrentLevel
    {
        get { return CurrentLevel; }
        set { CurrentLevel = value; }
    }
    public ClickSpawnedRabbitEvent()
    {
        type = eventType.ClickSpawnedRabbitEvent;
    }
}
