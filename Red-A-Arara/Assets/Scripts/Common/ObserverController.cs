using UnityEngine;

//Wants to know when another object does something interesting 
public abstract class ObserverController : MonoBehaviour
{
    public abstract void OnNotify();
}