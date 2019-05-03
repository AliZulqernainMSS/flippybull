using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UnityEventsExtension
{
    
}

public class ExtendedEvent : UnityEvent{}
public class ExtendedEvent<T> : UnityEvent<T>{}
public class ExtendedEvent<T1, T2> : UnityEvent<T1, T2>{}
