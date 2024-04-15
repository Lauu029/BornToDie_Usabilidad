using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilePersistence : IPersistence
{
    private Queue<TrackerEvent> eventQueue;

    public FilePersistence()
    {
        eventQueue = new Queue<TrackerEvent>();
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
            //TODO: Save into file
            throw new NotImplementedException();
        }
    }

}
