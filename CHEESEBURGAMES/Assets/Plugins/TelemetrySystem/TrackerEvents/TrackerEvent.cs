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

    //Atributos comunes que tienen todos los eventos, el tracker debe rellenarlos en TrackEvent()
    #region Common_Events_Attributes
    protected long timestamp;
    protected string event_ID;
    protected string session_ID;
    #region Getters&Setters
    public long Timestamp { set { timestamp = value; } }
    public string Event_ID { set { event_ID = value;  } }
    public string Session_ID { set { session_ID = value; } }
    #endregion
    #endregion


    public virtual string ToJson() {
        return "NOT_IMPLEMENTED_YET";
    }
}
