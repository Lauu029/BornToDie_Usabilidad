using System.Collections;
using System.Collections.Generic;

public interface IPersistence
{
    public void Send(TrackerEvent tEvent);

    public void SendFlush();

    public void Open(int updateMilliseconds);

    public void Close();
}

