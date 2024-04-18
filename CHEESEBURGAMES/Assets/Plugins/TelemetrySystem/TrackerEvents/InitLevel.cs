using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class InitLevel : TrackerEvent
{
    public int CurrentLevel { get; set; }

    public InitLevel()
    {
        type = eventType.InitLevelEvent;
    }

    public override string ToJson()      
    {
        return base.ToJson();
    }
}
