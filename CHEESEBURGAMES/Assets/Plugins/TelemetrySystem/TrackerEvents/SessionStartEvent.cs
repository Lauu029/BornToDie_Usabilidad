using System.Collections;
using System.Collections.Generic;

public class SessionStartEvent : TrackerEvent
{
    public SessionStartEvent()
    {
        Type = eventType.SessionStartEvent;
    }
}
