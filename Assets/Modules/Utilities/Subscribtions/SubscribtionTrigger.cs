using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SubscribtionTrigger<T> : MonoBehaviour
{
    public SubscribtionTriggerEvent<T> triggerEvent = new SubscribtionTriggerEvent<T> ();

    public virtual void FireSubscribtionTriggerEvent(T data)
    {
        triggerEvent.Invoke(data);
    }

}

public class SubscribtionTriggerEvent<T> : UnityEvent<T> { }
