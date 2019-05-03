using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event_Vector3", menuName = "ScriptableObjects/Events/Event_Vector3", order = 1)]
public class SOEvent_Vector3 : ScriptableObject
{
    private List<SOEventListener_Vector3> listeners = new List<SOEventListener_Vector3>();

    [SerializeField]
    private string descriptionText = "Enter event description here...";
    public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

    [SerializeField]
    private bool showDebugMessages = false;
    public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

    public void Raise(Vector3 value)
    {
        if (showDebugMessages)
        {
            Debug.Log(this.name + "Event Raised");
        }

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(SOEventListener_Vector3 listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(SOEventListener_Vector3 listener)
    {
        listeners.Remove(listener);
    }
}


