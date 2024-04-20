using System.Collections;
using System.Collections.Generic;

public class Json_Serializer : ISerializer
{
    public string EndFileFormat()
    {
        return "{}]}";
    }

    public string GetFileExtension()
    {
        return ".json";
    }

    public string InitFileFormat()
    {
        return "{\n \"Events\": [\n";
    }

    public string serialize(TrackerEvent trackerEvent)
    {
        return trackerEvent.ToJson()+","+"\n";
    }

}
