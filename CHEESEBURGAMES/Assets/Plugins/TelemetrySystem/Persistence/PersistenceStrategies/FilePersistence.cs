using System;
using System.Collections.Generic;

public class FilePersistence : IPersistence
{
    private Queue<TrackerEvent> eventQueue;
    private ISerializer serializer;

    public FilePersistence(ISerializer serializer)
    {
        eventQueue = new Queue<TrackerEvent>();
        this.serializer = serializer;

        // crear el fichero
        // nombre con el timestamp

        // open el fichero y guardar el manejador
    }

    public void Send(TrackerEvent tEvent)
    {
        eventQueue.Enqueue(tEvent);
    }

    public void Flush()
    {
        while (eventQueue.Count > 0)
        {
            TrackerEvent tEvent = eventQueue.Dequeue();
            string serializedEvent = this.serializer.serialize(tEvent);
            // TODO: Save into file (no abrir y cerrar el archivo todo el rato)
            throw new NotImplementedException();
        }
    }

}
