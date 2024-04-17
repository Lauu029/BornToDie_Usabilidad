using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

    public static bool Init(PersistenceType persistenceType, SerializationType serializationType) //Eventos de inicio de sesión, plataforma, SO...
    {
        Debug.Assert(instance== null);

        instance= new Tracker();
        //Al ser monobehaviour no hay que crear nueva instancia
        //Creación de una posible hebra que volcará los datos
        instance.ChoosePersistenceStrategy(persistenceType);
      

        return true;
    }

    public static bool End()
    {
        Debug.Assert(instance!= null);
        //Volcado de los datos restantes
        //Cierre de la posible hebra 

        instance=null;
        return true;
    }
    #endregion

    IPersistence persistenceStrategy;

    // Puede ser público si queremos habilitar que se cambie el tipo de persistencia a mitad del tracker
    private void ChoosePersistenceStrategy(PersistenceType pType)
    {
        switch (pType) 
        { 
        case PersistenceType.FILE:
            persistenceStrategy = new FilePersistence();
            break;
        case PersistenceType.SERVER:
            //TODO server persistence
            // persistenceStrategy = new ServerPeristence()
            persistenceStrategy = new FilePersistence();
            break;
        default:
            break;
        }
    }


    public void TrackEvent(TrackerEvent tEvent)
    {
        //Rellenar timestamp, event_ID, session_ID... del evento antes de enviarlo a la cola
        //timestamp posix desde 1 de Enero de 1970:
        DateTimeOffset now = DateTimeOffset.Now;
        tEvent.Timestamp = now.ToUnixTimeSeconds();

        persistenceStrategy.Send(tEvent);
    }
}