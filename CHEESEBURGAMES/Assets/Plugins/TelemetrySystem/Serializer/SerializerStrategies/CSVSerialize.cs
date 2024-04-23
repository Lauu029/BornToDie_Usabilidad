using System.Collections;
using System.Collections.Generic;

public class CSVSerialize : ISerializer
{
    private List<string> variablesList = new List<string> { "Event_ID", "Event_Type", "Session_ID", "Timestamp", "CurrentLevel" };
    private const string separator = ";";
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

        string initialString = "";
        for (int i = 0; i < variablesList.Count-1; i++)
        {
            initialString += variablesList[i] + separator;
        }
        initialString += variablesList[variablesList.Count-1];

        return initialString;
    }

    public string serialize(TrackerEvent trackerEvent)
    {
        return trackerEvent.ToCSV(ref variablesList);
    }
}
