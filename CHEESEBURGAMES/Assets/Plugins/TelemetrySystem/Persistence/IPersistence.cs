using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPersistence
{
    public void Send(TrackerEvent tEvent);

    public void Flush();
}

