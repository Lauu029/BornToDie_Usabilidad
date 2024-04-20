using System.Collections;
using System.Collections.Generic;

public class SessionEndEvent : TrackerEvent
{
    public SessionEndEvent() 
    {
        type = eventType.SessionEndEvent;
    }
}
