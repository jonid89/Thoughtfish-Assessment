using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class PlaybackCursor : MonoBehaviour
{
    public LayerMask buttonLayer;
    public LayerMask popupLayer;
    private ButtonView currentButton;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Observable.EveryUpdate()
            .Subscribe(_ => DetectButtonUnderCursor())
            .AddTo(this);
    }

    public void MoveCursor(Vector2 position){
        this.transform.position = position;
    }

private void DetectButtonUnderCursor()
{
    Vector2 rayOrigin = rectTransform.position;
    Vector2 rayDirection = Vector2.up; // Example direction (adjust as needed)
    float rayDistance = 1f; // Example distance (adjust as needed)

    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

    if (hit.collider != null)
    {
        Popup popup = hit.collider.GetComponent<Popup>();
        if (popup != null)
        {
            // The ray hit a Popup object
            Button popupButton = popup.GetComponent<Button>();
            Debug.Log("popUpButton hitted :" + popupButton.name);
            if (popupButton != null)
            {
                // The Popup object has a Button component
                popupButton.onClick.Invoke();
            }
        }
        else
        {
            ButtonView buttonView = hit.collider.GetComponent<ButtonView>();
            if (buttonView != null)
            {
                // The ray hit a ButtonView object
                buttonView.PlaybackCursorPosition(this.gameObject.transform.position);
                if (currentButton != buttonView)
                {
                    // The cursor entered a new button
                    currentButton?.PlaybackCursorExited();
                    currentButton = buttonView;
                    currentButton.PlaybackCursorEntered();
                }
            }
        }
    }
    else
    {
        // The ray did not hit any UI object
        currentButton?.PlaybackCursorExited();
        currentButton = null;
    }
}




}


