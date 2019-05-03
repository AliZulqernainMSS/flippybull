using UnityEngine;
using UnityEngine.Events;

public static class SubscribtionMethods
{

    public static void AddOnCollisionEnterListener(this Component component, UnityAction<Collision> callback)
    {
        component.gameObject.AddSubscriptionListener<PhysicsCollisionStarted, Collision>(callback);
    }

    public static void RemoveOnCollisionEnterListener(this Component component, UnityAction<Collision> callback)
    {
        component.gameObject.RemoveSubscriptionListener<PhysicsCollisionStarted, Collision>(callback);
    }

    public static void AddOnCollisionStayListener(this Component component, UnityAction<Collision> callback)
    {
        component.gameObject.AddSubscriptionListener<PhysicsCollisionStay, Collision>(callback);
    }

    public static void RemoveOnCollisionStayListener(this Component component, UnityAction<Collision> callback)
    {
        component.gameObject.RemoveSubscriptionListener<PhysicsCollisionStay, Collision>(callback);
    }

    public static void AddOnTiggerEnterListener(this Component component, UnityAction<Collider> callback)
    {
        component.gameObject.AddSubscriptionListener<PhysicsTriggerStarted, Collider>(callback);
    }

    public static void RemoveOnTiggerEnterListener(this Component component, UnityAction<Collider> callback)
    {
        component.gameObject.RemoveSubscriptionListener<PhysicsTriggerStarted, Collider>(callback);
    }

    public static void AddOnTiggerStayListener(this Component component, UnityAction<Collider> callback)
    {
        component.gameObject.AddSubscriptionListener<PhysicsTriggerStay, Collider>(callback);
    }

    public static void RemoveOnTiggerStayListener(this Component component, UnityAction<Collider> callback)
    {
        component.gameObject.RemoveSubscriptionListener<PhysicsTriggerStay, Collider>(callback);
    }

    public static void AddOnTiggerExitListener(this Component component, UnityAction<Collider> callback)
    {
        component.gameObject.AddSubscriptionListener<PhysicsTriggerExit, Collider>(callback);
    }

    public static void RemoveOnTiggerExitListener(this Component component, UnityAction<Collider> callback)
    {
        component.gameObject.RemoveSubscriptionListener<PhysicsTriggerExit, Collider>(callback);
    }

    public static T1 FindOrAddSubscriptionTrigger<T1, T2>(this GameObject gameObject) where T1 : SubscribtionTrigger<T2>
    {
        T1 trigger = gameObject.GetComponent<T1>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<T1>();
        }
        return trigger;
    }

    private static void AddSubscriptionListener<T1, T2>(this GameObject gameObject, UnityAction<T2> callback) where T1 : SubscribtionTrigger<T2>
    {
        T1 trigger = gameObject.FindOrAddSubscriptionTrigger<T1, T2>();
        trigger.triggerEvent.AddListener(callback);
    }

    private static void RemoveSubscriptionListener<T1, T2>(this GameObject gameObject, UnityAction<T2> callback) where T1 : SubscribtionTrigger<T2>
    {
        T1 trigger = gameObject.FindOrAddSubscriptionTrigger<T1, T2>();
        trigger.triggerEvent.RemoveListener(callback);
    }
}
