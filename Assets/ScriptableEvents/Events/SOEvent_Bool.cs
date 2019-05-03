using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event_Bool", menuName = "ScriptableObjects/Events/Event_Bool", order = 1)]
public class SOEvent_Bool : ScriptableObject
{
    private List<SOEventListener_Bool> listeners = new List<SOEventListener_Bool>();

    [SerializeField]
    private string descriptionText = "Enter event description here...";
    public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

    [SerializeField]
    private bool showDebugMessages = false;
    public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

    public void Raise(bool value)
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

    public void RegisterListener(SOEventListener_Bool listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(SOEventListener_Bool listener)
    {
        listeners.Remove(listener);
    }
}

