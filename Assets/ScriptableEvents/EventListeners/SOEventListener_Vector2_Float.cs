using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
    public class SOEventListener_Vector2_Float : MonoBehaviour
    {
        public SOEvent_Vector2_Float Event;
        public Vector2FloatEvent Response;

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

        public void OnEventRaised(Vector3 value, float num)
        {
            for (int i = 0; i < Response.GetPersistentEventCount(); i++)
            {
                if (Event.ShowDebugMessages)
                {
                    Debug.Log(Event.name + " raised: " + this.gameObject.name + " raising method " + Response.GetPersistentMethodName(i) + " with the parameter " + value);
                }
            }
            Response.Invoke(value, num);
        }
    }

    [System.Serializable]
    public class Vector2FloatEvent : UnityEvent<Vector2, float> { }
}