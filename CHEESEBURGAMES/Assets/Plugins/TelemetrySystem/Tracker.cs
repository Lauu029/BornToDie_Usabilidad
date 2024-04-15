using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PersistenceType
{
    FILE_PERSISTENCE = 0
}

public class Tracker : MonoBehaviour
{
    #region ExplicitInit_Singleton
    private static Tracker instance = null;
    private static bool initialized = false;
    public static Tracker Instance
    {
        get { 
            Assert.IsTrue(initialized);
            return instance; 
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static bool Init(PersistenceType persistenceType) //Eventos de inicio de sesión, plataforma, SO...
    {
        Assert.IsTrue(!initialized);
        //Al ser monobehaviour no hay que crear nueva instancia
        //Creación de una posible hebra que volcará los datos
        instance.ChoosePersistenceStrategy(persistenceType);

        initialized= true;
        return true;
    }

    public static bool End()
    {
        Assert.IsTrue(initialized);
        //Volcado de los datos restantes
        //Cierre de la posible hebra 
        initialized= false;
        return true;
    }
    #endregion

    IPersistence persistenceStrategy;

    // Puede ser público si queremos habilitar que se cambie el tipo de persistencia a mitad del tracker
    private void ChoosePersistenceStrategy(PersistenceType pType)
    {
        switch (pType) 
        { 
        case PersistenceType.FILE_PERSISTENCE:
                persistenceStrategy = new FilePersistence();
                break;
        default:
                break;
        }
    }

    public void TrackEvent(TrackerEvent tEvent)
    {
        //Rellenar timestamp, event_ID, session_ID... del evento antes de enviarlo a la cola
    
        persistenceStrategy.Send(tEvent);
    }
}