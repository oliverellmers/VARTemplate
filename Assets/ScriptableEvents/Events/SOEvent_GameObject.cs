using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event_With_GameObject", menuName = "ScriptableObjects/Events/Event_With_GameObject", order = 1)]
public class SOEvent_GameObject : ScriptableObject
{
    private List<SOEventListener_GameObject> listeners = new List<SOEventListener_GameObject>();

    [SerializeField]
    private string descriptionText = "Enter event description here...";
    public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

    [SerializeField]
    private bool showDebugMessages = false;
    public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

    public void Raise(GameObject value)
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

    public void RegisterListener(SOEventListener_GameObject listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(SOEventListener_GameObject listener)
    {
        listeners.Remove(listener);
    }
}

