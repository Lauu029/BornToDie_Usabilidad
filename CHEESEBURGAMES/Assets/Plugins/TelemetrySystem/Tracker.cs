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

    public static bool Init(PersistenceType persistenceType, SerializationType serializationType) //Eventos de inicio de sesión, plataforma, SO...
    {
        Debug.Assert(instance == null);

        instance = new Tracker();
        //Creación de una posible hebra que volcará los datos
        instance.ChooseSerializationStrategy(serializationType);
        instance.ChoosePersistenceStrategy(persistenceType);

        // Decidir el ID de sesión único 
        instance.GenerateUniqueID();

        // TODO : Evento de inicio de sesión

        // Programar cada cuanto tiempo se hace flush
        instance.SetUpTimer(1);

        return true;
    }

    public static bool End()
    {
        Debug.Assert(instance!= null);

        // TODO : Evento de fin de sesión

        //Volcado de los datos restantes
        //Cierre de la posible hebra 

        instance=null;
        return true;
    }
    #endregion

    IPersistence persistenceStrategy;
    ISerializer serializationStrategy;

    String sessionID;
    Timer timer;

    // Prepara el timer para que se haga flush cada X minutos
    private void SetUpTimer(int minutes)
    {
        if (this.timer != null)
        {
            this.timer.Dispose();
            this.timer = null;
        }

        this.timer = new Timer(this.PeriodicFlushEvents, null, 0, minutes);
    }

    // Puede ser público si queremos habilitar que se cambie el tipo de persistencia a mitad del tracker
    private void ChoosePersistenceStrategy(PersistenceType pType)
    {
        switch (pType) 
        { 
        case PersistenceType.FILE:
            this.persistenceStrategy = new FilePersistence(this.serializationStrategy);
            break;
        case PersistenceType.SERVER:
            //TODO server persistence
            // persistenceStrategy = new ServerPeristence()
            this.persistenceStrategy = new FilePersistence(this.serializationStrategy);
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
                this.serializationStrategy = new JsonSerializer();
                break;
            case SerializationType.CSV:
                // TODO csv serialization
                // serializationStrategy = new CsvSerializer();
                this.serializationStrategy = new JsonSerializer();
                break;
            default:
                break;
        }
    }

    private void GenerateUniqueID()
    {
        sessionID = Guid.NewGuid().ToString();
    }

    // TODO : preguntar por el nombre de este método
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
        persistenceStrategy.Flush();
    }

    private void PeriodicFlushEvents(object state)
    {
        this.FlushEvents();
    }
}