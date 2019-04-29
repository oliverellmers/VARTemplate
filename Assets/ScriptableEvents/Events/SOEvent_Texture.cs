using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "Event_Texture", menuName = "ScriptableObjects/Events/Event_Texture", order = 1)]
    public class SOEvent_Texture : ScriptableObject
    {
        private List<SOEventListener_Texture> listeners = new List<SOEventListener_Texture>();

        [SerializeField]
        private string descriptionText = "Enter event description here...";
        public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

        [SerializeField]
        private bool showDebugMessages = false;
        public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

        public void Raise(Texture value)
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

        public void RegisterListener(SOEventListener_Texture listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(SOEventListener_Texture listener)
        {
            listeners.Remove(listener);
        }
    }
}
