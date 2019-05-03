using UnityEngine;
using UnityEngine.Events;

public class SOEventListener_Bool : MonoBehaviour
{
    public SOEvent_Bool Event;
    public BoolEvent Response;

    private void OnEnable()
    {
        if (Event != null)
        {
            Event.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if (Event != null)
        {
            Event.UnRegisterListener(this);
        }
    }

    public void OnEventRaised(bool value)
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
public class BoolEvent : UnityEvent<bool> { }

