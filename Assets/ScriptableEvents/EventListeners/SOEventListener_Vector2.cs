using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
    public class SOEventListener_Vector2 : MonoBehaviour
    {
        public SOEvent_Vector2 Event;
        public Vector2Event Response;

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

        public void OnEventRaised(Vector2 value)
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
    public class Vector2Event : UnityEvent<Vector2> { }
}


