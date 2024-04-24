using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public enum PersistenceType
{
    FILE = 0,
    SERVER
}

public enum SerializationType
{
    JSON = 0,
    CSV
}

public class Tracker
{
    #region ExplicitInit_Singleton
    private static Tracker instance = null;

    public static Tracker Instance
    {
        get { 
            Debug.Assert(instance != null);
            return instance; 
        }
    }

    private Tracker() { } // Ocultar el constructor

    public static bool Init(PersistenceType persistenceType, SerializationType serializationType)
    {
        Debug.Assert(instance == null);

        instance = new Tracker();

        instance.eventFactory= new EventFactory();
        instance.ChooseSerializationStrategy(serializationType);
        instance.ChoosePersistenceStrategy(persistenceType);

        instance.persistenceStrategy.Open();

        // Decidir el ID de sesión único 
        instance.GenerateUniqueID();

        // Evento de inicio de sesión
        instance.SendSessionStartEvent();

        return true;
    }

    public static bool End()
    {
        Debug.Assert(instance!= null);

        // Finalizar la sesión
        instance.SendSessionEndEvent();

        instance = null;
        return true;
    }
    #endregion

    IPersistence persistenceStrategy;
    ISerializer serializationStrategy;

    EventFactory eventFactory;

    String sessionID;

    // Puede ser público si queremos habilitar que se cambie el tipo de persistencia a mitad del tracker
    private void ChoosePersistenceStrategy(PersistenceType pType)
    {
        switch (pType) 
        { 
        case PersistenceType.FILE:
            this.persistenceStrategy = new FilePersistence(this.serializationStrategy);
            break;
        case PersistenceType.SERVER:
                // TODO server persistence
                this.persistenceStrategy = new ServerPersistence(this.serializationStrategy);
            break;
        default:
            break;
        }
    }

    private void ChooseSerializationStrategy(SerializationType sType)
    {
        switch (sType)
        {
            case SerializationType.JSON:
                this.serializationStrategy = new Json_Serializer();
                break;
            case SerializationType.CSV:
                this.serializationStrategy = new CSVSerialize();
                break;
            default:
                break;
        }
    }

    private void GenerateUniqueID()
    {
        sessionID = Guid.NewGuid().ToString();
    }

    private void SendSessionStartEvent()
    {
        TrackEvent(new SessionStartEvent());
    }
    private void SendSessionEndEvent()
    {
        TrackEvent(new SessionEndEvent());

        // Volcado de los datos restantes
        FlushEvents();

        // Cierre de la posible hebra 
        persistenceStrategy.Close();
    }

    public void TrackEvent(TrackerEvent tEvent)
    {
        // Rellenar timestamp, event_ID, session_ID... del evento antes de enviarlo a la cola

        tEvent.Session_ID = sessionID;

        tEvent.Event_ID = Guid.NewGuid().ToString();

        // timestamp posix desde 1 de Enero de 1970:
        DateTimeOffset now = DateTimeOffset.Now;
        tEvent.Timestamp = now.ToUnixTimeSeconds();

        persistenceStrategy.Send(tEvent);
    }

    // Envia todos los eventos almacenados en la cola
    public void FlushEvents()
    {
        persistenceStrategy.SendFlush();
    }

    public EventFactory GetEventFactory() { return this.eventFactory; }
}