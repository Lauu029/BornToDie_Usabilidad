using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using UnityEngine;

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


    public string ToJson() {
        // aqui deberia usar el serializer que esté activo
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType());
        string json = "";
        // Crear un MemoryStream para escribir el JSON serializado
        using (MemoryStream stream = new MemoryStream())
        {
            // Serializar el objeto y escribirlo en el MemoryStream
            serializer.WriteObject(stream, this);

            // Convertir el MemoryStream a una cadena JSON
            json = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            Debug.Log(json);
        }
        return json;
        //return "NOT_IMPLEMENTED_YET";
    }
}
