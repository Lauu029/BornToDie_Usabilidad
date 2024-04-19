using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickSpawnedRabbitEvent : TrackerEvent
{
    int currentLevel;
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }
    public ClickSpawnedRabbitEvent()
    {
        type = eventType.ClickSpawnedRabbitEvent;
    }
}
