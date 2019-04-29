using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "Event_Transform", menuName = "ScriptableObjects/Events/Event_Transform", order = 1)]
    public class SOEvent_Transform : ScriptableObject
    {
        private List<SOEventListener_Transform> listeners = new List<SOEventListener_Transform>();

        [SerializeField]
        private string descriptionText = "Enter event description here...";
        public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

        [SerializeField]
        private bool showDebugMessages = false;
        public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

        public void Raise(Transform value)
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

        public void RegisterListener(SOEventListener_Transform listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(SOEventListener_Transform listener)
        {
            listeners.Remove(listener);
        }
    }
}

