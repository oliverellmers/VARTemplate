using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenRecorderUIController : MonoBehaviour
{
    
    public Button ShowPanelButton;
    public Button HidePanelButton;
    public Button ShareVideoButton;
    public Button CancelShareVideoButton;
    public Button OpenGalleryButton;

    public CanvasGroup ShareDialogue;


    private NatCorder.Examples.ReplayCam ReplayCam;
    private float transitionTime = 0.25f;
    private bool panelIsVisible;
    private bool shareDialogueIsVisible;


    // Start is called before the first frame update
    void Start()
    {
        ReplayCam = FindObjectOfType<NatCorder.Examples.ReplayCam>();
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
            transform.GetComponent<CanvasGroup>().interactable = true;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.GetComponent<CanvasGroup>().alpha = 1f;
            transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), transitionTime);
            panelIsVisible = true;
        }
        else {
            transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, -transform.GetComponent<RectTransform>().sizeDelta.y), transitionTime).OnComplete(()=>{
                transform.GetComponent<CanvasGroup>().interactable = false;
                transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
                transform.GetComponent<CanvasGroup>().alpha = 0f;
                panelIsVisible = false;
            });
        }
        yield return new WaitForSeconds(transitionTime);
    }

    public void ShareDialogueVisibility(bool b, float t) {
        StartCoroutine(DOShareDialogueVisibility(b, t));
    }

    IEnumerator DOShareDialogueVisibility(bool b, float t) {
        if (b)
        {
            ShareDialogue.DOFade(1f, t);
        }
        else {
            ShareDialogue.DOFade(0f, t);
        }

        ShareDialogue.interactable = b;
        ShareDialogue.blocksRaycasts = b;
        shareDialogueIsVisible = b;

        yield return new WaitForSeconds(transitionTime);
    }


    private void PickVideo()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                // Play the selected video
                Handheld.PlayFullScreenMovie("file://" + path);
            }
        }, "Select a video");

        Debug.Log("Permission result: " + permission);
    }
}
