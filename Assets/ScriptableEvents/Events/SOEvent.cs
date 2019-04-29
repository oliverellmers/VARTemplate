using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Events/Event", order = 1)]
    public class SOEvent : ScriptableObject
    {
        private List<SOEventListener> listeners = new List<SOEventListener>();

        [SerializeField]
        private string descriptionText = "Enter event description here...";
        public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

        [SerializeField]
        private bool showDebugMessages = false;
        public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

        public void Raise()
        {
            if (showDebugMessages)
            {
                Debug.Log(this.name + "Event Raised");
            }

            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(SOEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(SOEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
