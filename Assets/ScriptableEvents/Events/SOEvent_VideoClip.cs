using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "Event_VideoClip", menuName = "ScriptableObjects/Events/Event_VectorClip", order = 1)]
    public class SOEvent_VideoClip : ScriptableObject
    {
        private List<SOEventListener_VideoClip> listeners = new List<SOEventListener_VideoClip>();

        [SerializeField]
        private string descriptionText = "Enter event description here...";
        public string DescriptionText { get { return descriptionText; } set { descriptionText = value; } }

        [SerializeField]
        private bool showDebugMessages = false;
        public bool ShowDebugMessages { get { return showDebugMessages; } set { showDebugMessages = value; } }

        public void Raise(VideoClip value)
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

        public void RegisterListener(SOEventListener_VideoClip listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(SOEventListener_VideoClip listener)
        {
            listeners.Remove(listener);
        }
    }
}
