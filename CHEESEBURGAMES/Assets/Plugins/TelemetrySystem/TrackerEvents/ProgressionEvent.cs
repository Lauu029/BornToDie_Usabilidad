using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class ProgressionEvent : TrackerEvent
{
    
    int dinero = 0;
  
    public int EventId { get; set; }

   
    public long EventTimestamp { get; set; }

    public int CurrentLevel { get; set; }
    public ProgressionEvent()
    {
        type = eventType.ProgressionEvent;
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

            // Imprimir la cadena JSON resultante ALERTAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            //NOOOOOOOOOOOOOOOOOO
            //NOOO
            Debug.Log(json);

        }
        return json;
    }
}
