﻿using UnityEngine;
using UnityEngine.Events;


public class SOEventListener_Vector3 : MonoBehaviour
{
    public SOEvent_Vector3 Event;
    public Vector3Event Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(Vector3 value)
    {
        for (int i = 0; i < Response.GetPersistentEventCount(); i++)
        {
            if (Event.ShowDebugMessages)
            {
                Debug.Log(Event.name + " raised: " + this.gameObject.name + " raising method " + Response.GetPersistentMethodName(i) + " with the parameter " + value);
            }
        }
        Response.Invoke(value);
    }
}

[System.Serializable]
public class Vector3Event : UnityEvent<Vector3> { }

