using UnityEngine;
using UnityEngine.Events;


public class SOEventListener_GameObject : MonoBehaviour
{
    public SOEvent_GameObject Event;
    public GameObjectEvent Response;

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

    public void OnEventRaised(GameObject value)
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
public class GameObjectEvent : UnityEvent<GameObject> { }


