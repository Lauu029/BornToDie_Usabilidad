using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickUIRabbit : TrackerEvent
{
    public int CurrentLevel { get; set; }
    public ClickUIRabbit()
    {
        type = eventType.ClickUIRabbitEvent;
    }
}
