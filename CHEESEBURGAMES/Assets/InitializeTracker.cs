using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeTracker : MonoBehaviour
{
    void Start()
    {
        // Inicializa el tracker, se indica en los par�metros el comportamiento
        Tracker.Init(PersistenceType.FILE, SerializationType.JSON);
    }
}
