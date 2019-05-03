using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using DG.Tweening;

public class ObjectSelectCanvasUIController : MonoBehaviour
{
    public Button ShowPanelButton;

    public CanvasGroup SelectionPanelCG;
    public RectTransform SelectionPanelRT;

    public HorizontalScrollSnap HorizontalScrollSnap;

    public SOEvent_Integer SOEvent_ObjectSelectionChanged;


    private float transitionTime = 0.25f;
    private bool panelIsVisible;
    private bool shareDialogueIsVisible;


    // Start is called before the first frame update
    void Start()
    {      
        PanelVisibility(false, 0f);
        ButtonsSetup();
    }

    private void ButtonsSetup() {
        ShowPanelButton.onClick.AddListener(() => {
            PanelVisibility(!panelIsVisible, transitionTime);

        });

        HorizontalScrollSnap.OnSelectionPageChangedEvent.AddListener((i)=> {
            ObjectSelectionPanelSwiped(i);
        });
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
            SelectionPanelCG.interactable = true;
            SelectionPanelCG.blocksRaycasts = true;
            SelectionPanelCG.alpha = 1f;
            SelectionPanelRT.DOAnchorPos(new Vector2(0f, 0f), t);
            panelIsVisible = true;
        }
        else {
            /*
            SelectionPanelRT.DOAnchorPos(new Vector2(0f, -SelectionPanelRT.sizeDelta.y), t).OnComplete(()=>{
            SelectionPanelCG.interactable = false;
            SelectionPanelCG.blocksRaycasts = false;
            SelectionPanelCG.alpha = 0f;
            panelIsVisible = false;
            });
            */

            SelectionPanelRT.DOAnchorPos(new Vector2(-Screen.width * 1.5f, 0f), t).OnComplete(() => {
                SelectionPanelCG.interactable = false;
                SelectionPanelCG.blocksRaycasts = false;
                SelectionPanelCG.alpha = 0f;
                panelIsVisible = false;
            });
        }
        yield return new WaitForSeconds(t);
    }

    private void ObjectSelectionPanelSwiped(int i) {
        SOEvent_ObjectSelectionChanged.Raise(i);
    }

}
