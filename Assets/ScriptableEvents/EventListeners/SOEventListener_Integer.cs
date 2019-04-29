using UnityEngine;
using UnityEngine.Events;


public class SOEventListener_Integer : MonoBehaviour
{
    public SOEvent_Integer Event;
    public IntegerEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(int value)
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
public class IntegerEvent : UnityEvent<int> { }

