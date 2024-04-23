using System;
using System.IO;
using System.Threading;

public class FilePersistence : APersistance
{
    const string path = "./TrackerOutputs/";

    private StreamWriter file;

    private Thread thread;

    public FilePersistence(ISerializer serializer) : base(serializer)
    {
       
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

    public override void Open()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // nombre con el timestamp
        string fileName = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + serializer.GetFileExtension();

        // crear el fichero
        // open el fichero y guardar el manejador
        file = new StreamWriter(Path.Combine(path, fileName));

        // inicio con el formato del serializador
        file.Write(serializer.InitFileFormat());
    }
}
