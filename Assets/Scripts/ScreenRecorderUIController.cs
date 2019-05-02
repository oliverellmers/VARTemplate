using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenRecorderUIController : MonoBehaviour
{
    public string SaveFolder = "";
    public Text MessageText;
    public Button ShowPanelButton;
    public Button HidePanelButton;
    public Button ShareVideoButton;
    public Button CancelShareVideoButton;
    public Button OpenGalleryButton;

    public CanvasGroup RecordingPanelCG;
    public CanvasGroup ShareDialogueCG;

    public RectTransform RecordingPanelRT;


    private NatCorder.Examples.ReplayCam ReplayCam;
    private float transitionTime = 0.25f;
    private bool panelIsVisible;
    private bool shareDialogueIsVisible;


    // Start is called before the first frame update
    void Start()
    {
        ReplayCam = FindObjectOfType<NatCorder.Examples.ReplayCam>();
        ReplayCam.saveFolder = SaveFolder;

        MessageText.transform.GetComponentInParent<CanvasGroup>().alpha = 0f;
        
        PanelVisibility(false, 0f);
        ShareDialogueVisibility(false, 0f);
        ButtonsSetup();
    }

    private void ButtonsSetup() {
        ShowPanelButton.onClick.AddListener(() => {
            PanelVisibility(!panelIsVisible, transitionTime);
            if (shareDialogueIsVisible) {
                ShareDialogueVisibility(false, transitionTime);
            }
        });

        HidePanelButton.onClick.AddListener(() => { PanelVisibility(false, transitionTime); });

        ShareVideoButton.onClick.AddListener(() => {
            ReplayCam.ShareImage();
            ShareDialogueVisibility(false, 0.25f);
        });

        CancelShareVideoButton.onClick.AddListener(() => {
            ReplayCam.DiscardVideo();
            ShareDialogueVisibility(false, transitionTime);
        });

        OpenGalleryButton.onClick.AddListener(() => { PickVideo(); });   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelVisibility(bool b, float t) {
        StartCoroutine(DOPanelVisibility(b, t));
    }
    IEnumerator DOPanelVisibility(bool b, float t) {
        if (b)
        {
            RecordingPanelCG.interactable = true;
            RecordingPanelCG.blocksRaycasts = true;
            RecordingPanelCG.alpha = 1f;
            RecordingPanelRT.DOAnchorPos(new Vector2(0f, 0f), t);
            panelIsVisible = true;
        }
        else {
            RecordingPanelRT.DOAnchorPos(new Vector2(0f, -RecordingPanelRT.sizeDelta.y), t).OnComplete(()=>{
            RecordingPanelCG.interactable = false;
            RecordingPanelCG.blocksRaycasts = false;
            RecordingPanelCG.alpha = 0f;
            panelIsVisible = false;
            });
        }
        yield return new WaitForSeconds(t);
    }

    public void ShareDialogueVisibility(bool b, float t) {
        StartCoroutine(DOShareDialogueVisibility(b, t));
    }

    IEnumerator DOShareDialogueVisibility(bool b, float t) {
        if (b)
        {
            ShareDialogueCG.DOFade(1f, t);
        }
        else {
            ShareDialogueCG.DOFade(0f, t);
        }

        ShareDialogueCG.interactable = b;
        ShareDialogueCG.blocksRaycasts = b;
        shareDialogueIsVisible = b;

        yield return new WaitForSeconds(t);
    }

    public void SetMessageText(string s) {
        MessageText.text = s;
        StartCoroutine(DOSetMessageText());
    }

    IEnumerator DOSetMessageText() {

        MessageText.transform.GetComponentInParent<CanvasGroup>().DOFade(1f, 0.125f);
        yield return new WaitForSeconds(2.0f);
        MessageText.transform.GetComponentInParent<CanvasGroup>().DOFade(0f, 1f);
        yield return new WaitForSeconds(1.0f);
    }


    private void PickVideo()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                // Play the selected video
                Handheld.PlayFullScreenMovie("file://" + path + "/" + SaveFolder);
            }
        }, "Select a video");

        Debug.Log("Permission result: " + permission);
    }

    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;

                Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy(texture, 5f);
            }
        }, "Select a PNG image", "image/png", maxSize);

        Debug.Log("Permission result: " + permission);
    }
}
