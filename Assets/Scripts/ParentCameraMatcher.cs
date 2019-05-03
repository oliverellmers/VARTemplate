using UnityEngine;
using System.Collections;

public class ParentCameraMatcher : MonoBehaviour
{
    void Update()
    {
        GetComponent<Camera>().fieldOfView = transform.parent.gameObject.GetComponent<Camera>().fieldOfView;
        GetComponent<Camera>().farClipPlane = transform.parent.gameObject.GetComponent<Camera>().farClipPlane;
    }
}