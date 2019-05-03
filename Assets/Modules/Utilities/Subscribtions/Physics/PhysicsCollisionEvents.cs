using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollisionStarted : SubscribtionTrigger<Collision>
{
    private void OnCollisionEnter(Collision collision)
    {
        FireSubscribtionTriggerEvent(collision);
    }
}

public class PhysicsCollisionStay : SubscribtionTrigger<Collision>
{
    private void OnCollisionStay(Collision collision)
    {
        FireSubscribtionTriggerEvent(collision);
    }
}

public class PhysicsCollisionExit : SubscribtionTrigger<Collision>
{
    private void OnCollisionExit(Collision collision)
    {
        FireSubscribtionTriggerEvent(collision);
    }
}

