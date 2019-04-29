using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event_String", menuName = "ScriptableObjects/Events/Event_String", order = 1)]
public class SOEvent_String : ScriptableObject
{
    private List<SOEventListener_String> listeners = new List<SOEventListener_String>();

    [SerializeField]
    private string descriptionText = "Enter event description here...";
    public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

    [SerializeField]
    private bool showDebugMessages = false;
    public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

    public void Raise(string value)
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

    public void RegisterListener(SOEventListener_String listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(SOEventListener_String listener)
    {
        listeners.Remove(listener);
    }
}


