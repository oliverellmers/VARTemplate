using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppSetup : MonoBehaviour
{
    public AppData allAppData;
    public Text SceneTitle;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(allAppData.StartScene);
        SceneTitle.text = allAppData.StartScene;
    }


    public AppData GetAppData()
    {
        return allAppData;
    }

    public void LogVuforiaTrackableEvent(bool b) {
        if (b)
        {
            Debug.Log("Vuforia Trackable Found!");
        }
        else {
            Debug.Log("Vuforia Trackable Lost!");
        }
    }
}
