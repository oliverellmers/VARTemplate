using UnityEngine;
using UnityEngine.Events;


public class SOEventListener_String : MonoBehaviour
{
    public SOEvent_String Event;
    public StringEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(string value)
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
public class StringEvent : UnityEvent<string> { }
