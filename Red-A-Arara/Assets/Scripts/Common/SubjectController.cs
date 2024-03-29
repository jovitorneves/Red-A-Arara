﻿using System.Collections.Generic;
using UnityEngine;

public class SubjectController : MonoBehaviour
{
    //A list with observers that are waiting for something to happen
    List<ObserverController> observers = new List<ObserverController>();

    //Send notifications if something has happened
    public void Notify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            //Notify all observers even though some may not be interested in what has happened
            //Each observer should check if it is interested in this event
            observers[i].OnNotify();
        }
    }

    //Add observer to the list
    public void AddObserver(ObserverController observer)
    {
        observers.Add(observer);
    }

    //Remove observer from the list
    public void RemoveObserver(ObserverController observer)
    {
    }
}
