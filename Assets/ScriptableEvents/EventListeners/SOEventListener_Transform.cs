using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
    public class SOEventListener_Transform : MonoBehaviour
    {
        public SOEvent_Transform Event;
        public TransformEvent Response;

        private void OnEnable()
        {
            if (Event != null)
            {
                Event.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            if (Event != null)
            {
                Event.UnRegisterListener(this);
            }
        }

        public void OnEventRaised(Transform value)
        {
            for (int i = 0; i < Response.GetPersistentEventCount(); i++)
            {
                if (Event.ShowDebugMessages)
                {
                    Debug.Log(Event.name + " raised: " + this.gameObject.name + " raising method " + Response.GetPersistentMethodName(i) + " with the parameter " + value);
                }
            }
            Response.Invoke(value);
        }
    }

    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform> { }
}


