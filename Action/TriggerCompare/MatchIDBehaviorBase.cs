using UnityEngine;
using UnityEngine.Events;

public class SimpleMatchID : IDBehavior
{
    protected ID OtherIdObj;
    public UnityEvent matchEvent, noMatchEvent;
    
    public virtual void OnTriggerEnter(Collider other)
    {
        OtherIdObj = other.GetComponent<IDBehavior>().idObj;

        if (idObj == OtherIdObj)
        {
            matchEvent.Invoke();
        }
        else
        {
            noMatchEvent.Invoke();
        }
    }
}