using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

public abstract class TrackerEvent
{
    public enum eventType
    {
        ProgressionEvent,
        ResourceEvent,
        SessionStartEvent,
        SessionEndEvent,
        InitLevelEvent,
        EndLevelEvent,
        ClickUIRabbitEvent,
        ClickSpawnedRabbitEvent,
        ClickGoEvent,
        ClickBornEvent
    }

    private eventType type;
    protected eventType Type
    {
        get { return type; }
        set { type = value; Event_Type = type.ToString(); }
    }

    //Atributos comunes que tienen todos los eventos, el tracker debe rellenarlos en TrackEvent()
    #region Common_Events_Attributes
    protected long timestamp;
    protected string event_ID="EVENT_ID";
    protected string session_ID="SESSION_ID";
    protected string event_type;
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
    public string Event_Type
    {
        get { return event_type; }
        set { event_type = value; }
    }
    #endregion
    #endregion

    private const string separator = ";";
    public string ToJson() {
        // aqui deberia usar el serializer que est� activo
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType());
        string json = "";
        // Crear un MemoryStream para escribir el JSON serializado
        using (MemoryStream stream = new MemoryStream())
        {
            // Serializar el objeto y escribirlo en el MemoryStream
            serializer.WriteObject(stream, this);

            // Convertir el MemoryStream a una cadena JSON
            json = Encoding.UTF8.GetString(stream.ToArray());
        }
        return json;
    }
    public string ToCSV(ref List<string> eventVariables)
    {

        // Obtener todas las propiedades p�blicas del objeto
        PropertyInfo[] properties = this.GetType().GetProperties();

        Dictionary<string, string> propertiesMap = new Dictionary<string, string>();
        string finalString = "";

        // Iterar sobre las propiedades
        foreach (PropertyInfo property in properties)
        {
            string propertyName = property.Name;

            // Obtener el valor de la propiedad
            object value = property.GetValue(this, null);
            string strValue = "";
            // Si el valor es nulo, agregar una cadena vac�a al CSV
            if (value != null)
            {
                strValue = value.ToString();
            }
            propertiesMap[propertyName] = strValue;
        }

        foreach (string listItem in eventVariables)
        {
            if (propertiesMap.ContainsKey(listItem))
            {
                finalString += propertiesMap[listItem] + separator;

                propertiesMap.Remove(listItem);
            }
            else finalString += separator;
        }

        /* Sin el separador al final de la linea
        foreach (string listItem in eventVariables)
        {
            if (propertiesMap.ContainsKey(listItem))
            {
                if (listItem != eventVariables[eventVariables.Count - 1])
                    finalString += propertiesMap[listItem] + separator;
                else
                    finalString += propertiesMap[listItem];
                propertiesMap.Remove(listItem);
            }
            else {
                if (listItem != eventVariables[eventVariables.Count - 1])
                    finalString += separator;
            }
        }*/

        finalString += "\n";
        return finalString;
    }
}
