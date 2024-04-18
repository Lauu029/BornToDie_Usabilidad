using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class InitLevel : TrackerEvent
{
    public int CurrentLevel { get; set; }

    public InitLevel()
    {
        type = eventType.InitLevelEvent;
    }

    public override string ToJson()      
    {
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProgressionEvent));
        string json = "";
        // Crear un MemoryStream para escribir el JSON serializado
        using (MemoryStream stream = new MemoryStream())
        {
            // Serializar el objeto y escribirlo en el MemoryStream
            serializer.WriteObject(stream, this);

            // Convertir el MemoryStream a una cadena JSON
             json = System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }
        return json;
    }
}
