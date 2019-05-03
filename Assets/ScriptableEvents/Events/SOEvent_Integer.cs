using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event_Integer", menuName = "ScriptableObjects/Events/Event_Integer", order = 1)]
public class SOEvent_Integer : ScriptableObject
{
    private List<SOEventListener_Integer> listeners = new List<SOEventListener_Integer>();

    [SerializeField]
    private string descriptionText = "Enter event description here...";
    public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

    [SerializeField]
    private bool showDebugMessages = false;
    public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

    public void Raise(int value)
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

    public void RegisterListener(SOEventListener_Integer listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(SOEventListener_Integer listener)
    {
        listeners.Remove(listener);
    }
}


