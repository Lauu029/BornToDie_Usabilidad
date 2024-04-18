using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
public class ClickSpawnedRabbit : TrackerEvent
{
    public int CurrentLevel { get; set; }
    public ClickSpawnedRabbit()
    {
        type = eventType.ClickSpawnedRabbitEvent;
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
