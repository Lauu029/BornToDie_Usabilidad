using System.Collections;
using System.Collections.Generic;

public class CSVSerialize : ISerializer
{
    List<string> eventVariables= new List<string>();
    const char separator = ';';
    public string EndFileFormat()
    {
        return "";
    }

    public string GetFileExtension()
    {
        return ".csv";
    }

    public string InitFileFormat()
    {
        string finalString = "";
        for(int i=0; i<eventVariables.Count-1; i++)
        {
            finalString += eventVariables[i] + separator;
        }
        finalString += eventVariables[eventVariables.Count-1];
        eventVariables.Clear();
        return finalString;
    }

    public string serialize(TrackerEvent trackerEvent)
    {
        return trackerEvent.ToCSV(ref eventVariables);
    }
}
