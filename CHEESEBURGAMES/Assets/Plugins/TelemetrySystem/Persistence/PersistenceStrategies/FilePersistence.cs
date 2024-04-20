using System;
using System.IO;

public class FilePersistence : APersistance
{
    const string path = "./TrackerOutputs/";

    private StreamWriter file;

    public FilePersistence(ISerializer serializer) : base(serializer)
    {
        // nombre con el timestamp
        string fileName = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + serializer.GetFileExtension();

        // crear el fichero
        // open el fichero y guardar el manejador
        file = new StreamWriter(path + fileName);
        
        // inicio con el formato del serializador
        file.Write(serializer.InitFileFormat());
    }

    public override void Flush()
    {
        while (eventQueue.Count > 0)
        {
            TrackerEvent tEvent = eventQueue.Dequeue();
            file.Write(serializer.serialize(tEvent));
        }     
    }

    public override void Close()
    {
        file.Write(serializer.EndFileFormat());
        file.Close();
    }
}
