using System.Collections;
using System.Collections.Generic;

public class Json_Serializer : ISerializer
{
    public string serialize(TrackerEvent trackerEvent)
    {
        return trackerEvent.ToJson();
    }

}
