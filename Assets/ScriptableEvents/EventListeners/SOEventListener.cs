using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class SOEventListener : MonoBehaviour
{
    public SOEvent Event;
    public UnityEvent Response = new UnityEvent();

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
       
    /// <summary>
    /// Only used when dynamically adding this component - so it can still subscribe to the event
    /// </summary>
    public void LateRegister()
    {
        if (Event != null)
        {
            Event.RegisterListener(this);
        }
    }

    public void OnEventRaised()
    {
        for (int i = 0; i < Response.GetPersistentEventCount(); i++)
        {
            if (Event.ShowDebugMessages)
            {
                Debug.Log(Event.name + " raised: " + this.gameObject.name + " raising method " + Response.GetPersistentMethodName(i));
            }
        }
        Response.Invoke();
    }
}

