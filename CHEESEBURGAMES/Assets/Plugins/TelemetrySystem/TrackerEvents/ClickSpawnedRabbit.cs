using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickSpawnedRabbit : TrackerEvent
{
    public int CurrentLevel { get; set; }
    public ClickSpawnedRabbit()
    {
        type = eventType.ClickSpawnedRabbitEvent;
    }
}
