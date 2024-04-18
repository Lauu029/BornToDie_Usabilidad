using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
public abstract class TrackerEvent
{
    public enum eventType
    {
        ProgressionEvent,
        ResourceEvent,
        InitLevelEvent,
        EndLevelEvent,
        ClickUIRabbitEvent,
        ClickSpawnedRabbitEvent,
        ClickGoEvent,
        ClickBornEvent
    }

    protected eventType type;

    //Atributos comunes que tienen todos los eventos, el tracker debe rellenarlos en TrackEvent()
    #region Common_Events_Attributes
    protected long timestamp;
    protected string event_ID;
    protected string session_ID;
    #region Getters&Setters

    public long Timestamp {
        get { return timestamp; }
        set { timestamp = value; } 
    }
    public string Event_ID
    {
        get { return event_ID; }
        set { event_ID = value;  } }
    public string Session_ID {
        get { return session_ID; }
        set { session_ID = value; }
    }
    #endregion
    #endregion


    public virtual string ToJson() {
        return "NOT_IMPLEMENTED_YET";
        // aqui deberia usar el serializer que esté activo
    }
}
