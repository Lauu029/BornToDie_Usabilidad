using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class EndLevelEvent : TrackerEvent
{
    private int currentLevel = 0;
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }
    public EndLevelEvent()
    {
        Type = eventType.EndLevelEvent;
    }
}
