using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private SO_GameObject objectToSpawn = null;

    private bool objectSpawned = false;

    /// <summary>
    /// Spawn assigned object when event is raised
    /// </summary>
    public void SpawnGameObject()
    {
        if (!objectSpawned)
        {
            if (objectToSpawn != null)
            {
                //Spawn the correct model and set it's name accordingly
                GameObject medicalObject = GameObject.Instantiate(objectToSpawn.value, transform.position, transform.rotation);
                medicalObject.transform.parent = this.gameObject.transform;
                medicalObject.name = objectToSpawn.value.name;
            }
        }
    }
}
