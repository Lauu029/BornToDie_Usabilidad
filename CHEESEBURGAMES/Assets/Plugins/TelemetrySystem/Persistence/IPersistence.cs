using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPersistence
{
    public void Send();

    public void Flush();
}

