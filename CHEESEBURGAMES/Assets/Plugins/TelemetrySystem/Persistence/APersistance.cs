using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class APersistance : IPersistence
{
    protected Queue<TrackerEvent> eventQueue;
    protected ISerializer serializer;

    public APersistance(ISerializer serializer)
    {
        this.serializer = serializer;
        eventQueue = new Queue<TrackerEvent>();
    }

    public virtual void Send(TrackerEvent tEvent)
    {
        eventQueue.Enqueue(tEvent);
    }

    public abstract void SendFlush();

    public abstract void Open(int updateMilliseconds);

    public abstract void Close();
}
