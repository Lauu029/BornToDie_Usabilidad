using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class EndLevel : TrackerEvent
{
    public int CurrentLevel { get; set; }
    public EndLevel()
    {
        type = eventType.EndLevelEvent;
    }
    public override string ToJson()      
    {
        return base.ToJson();
    }
}
