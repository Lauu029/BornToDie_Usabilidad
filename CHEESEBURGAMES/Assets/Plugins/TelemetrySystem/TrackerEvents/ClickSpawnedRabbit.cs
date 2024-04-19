using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickSpawnedRabbit : TrackerEvent
{
    public int CurrentLevel
    {
        get { return CurrentLevel; }
        set { CurrentLevel = value; }
    }
    public ClickSpawnedRabbit()
    {
        type = eventType.ClickSpawnedRabbitEvent;
    }
}
