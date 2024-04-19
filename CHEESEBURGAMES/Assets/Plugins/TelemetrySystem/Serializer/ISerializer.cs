using System.Collections;
using System.Collections.Generic;

public interface ISerializer
{
    public string serialize(TrackerEvent trackerEvent);
    public string InitFileFormat();
    public string GetFileExtension();
    public string EndFileFormat();
}
