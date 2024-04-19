using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class InitLevel : TrackerEvent
{
    public int CurrentLevel
    {
        get { return CurrentLevel; }
        set { CurrentLevel = value; }
    }

    public InitLevel()
    {
        type = eventType.InitLevelEvent;
    }

}
