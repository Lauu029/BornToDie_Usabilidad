using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeTracker : MonoBehaviour
{
    void Start()
    {
        Tracker.Init(PersistenceType.FILE, SerializationType.JSON);
    }
}
