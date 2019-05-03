using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTriggerStarted : SubscribtionTrigger<Collider>
{
    private void OnTriggerEnter(Collider other)
    {
        FireSubscribtionTriggerEvent(other);
    }
}

public class PhysicsTriggerStay : SubscribtionTrigger<Collider>
{
    private void OnTriggerStay(Collider other)
    {
        FireSubscribtionTriggerEvent(other);
    }
}

public class PhysicsTriggerExit : SubscribtionTrigger<Collider>
{
    private void OnTriggerExit(Collider other)
    {
        FireSubscribtionTriggerEvent(other);
    }
}