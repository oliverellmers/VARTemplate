using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppSetup : MonoBehaviour
{
    public AppData allAppData;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(allAppData.StartScene);
        
    }


    public AppData GetAppData()
    {
        return allAppData;
    }

    private void SetupUI() {
        foreach (CanvasGroup cg in allAppData.UILayers) {
            if (allAppData.StartScene != cg.gameObject.name)
            {
                cg.interactable = false;
                cg.blocksRaycasts = false;
                cg.alpha = 0;
            }
            else {
                cg.interactable = true;
                cg.blocksRaycasts = true;
                cg.alpha = 1;
            }
            
        }
        
    }
}
