using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrackerEvent
{
    public enum eventType
    {
        ProgressionEvent,
        ResourceEvent
    }

    protected eventType type;

    public abstract string ToJson();
}
