using System.Collections;
using System.Collections.Generic;

public class ProgressionEvent : TrackerEvent
{
    public ProgressionEvent()
    {
        type = eventType.ProgressionEvent;
    }

    public override string ToJson()
    {
        throw new System.NotImplementedException();
    }
}
