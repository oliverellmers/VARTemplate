using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "Event_Vector2", menuName = "ScriptableObjects/Events/Event_Vector2", order = 1)]
    public class SOEvent_Vector2 : ScriptableObject
    {
        private List<SOEventListener_Vector2> listeners = new List<SOEventListener_Vector2>();

        [SerializeField]
        private string descriptionText = "Enter event description here...";
        public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

        [SerializeField]
        private bool showDebugMessages = false;
        public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

        public void Raise(Vector2 value)
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

        public void RegisterListener(SOEventListener_Vector2 listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(SOEventListener_Vector2 listener)
        {
            listeners.Remove(listener);
        }
    }
}

