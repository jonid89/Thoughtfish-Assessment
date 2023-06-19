using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class PlaybackCursor : MonoBehaviour
{
    public LayerMask buttonLayer;
    public LayerMask popupLayer;
    private ButtonView currentButton;
    private RectTransform rectTransform;
    private IDisposable updateDisposable;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        updateDisposable = this.UpdateAsObservable()
            .Subscribe(_ => DetectButtonUnderCursor());
    }

    private void OnDisable()
    {
        updateDisposable?.Dispose();
    }

    public void MoveCursor(Vector2 position)
    {
        transform.position = position;
    }

    private void DetectButtonUnderCursor()
    {
        Vector2 rayOrigin = rectTransform.position;
        Vector2 rayDirection = Vector2.up; // Example direction (adjust as needed)
        float rayDistance = 5f; // Example distance (adjust as needed)

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, rayDirection, rayDistance);

        // Check for Popup buttons first
        bool popupButtonHit = false;
        Debug.Log("hits.Length: " + hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            Debug.Log("Current hit: " + hits[i].collider.tag);
            if (popupLayer == hit.collider.gameObject.layer)
            {
                Button popupButton = hit.collider.gameObject.GetComponent<Button>();
                Debug.Log("The ray hit a Popup object with a Button component");
                if (popupButton != null)
                {
                    popupButton.onClick.Invoke();
                    popupButtonHit = true;
                    break;
                }
            }
        }

        if (!popupButtonHit)
        {
            // If no Popup button was hit, check for ButtonView objects
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (buttonLayer == (buttonLayer | (1 << hit.collider.gameObject.layer)))
                {
                    ButtonView buttonView = hit.collider.GetComponent<ButtonView>();
                    buttonView.PlaybackCursorPosition(this.gameObject.transform.position);
                    if (buttonView != null)
                    {
                        // The ray hit a ButtonView object
                        if (currentButton != buttonView)
                        {
                            // The cursor entered a new button
                            currentButton?.PlaybackCursorExited();
                            currentButton = buttonView;
                            currentButton.PlaybackCursorEntered();
                        }
                        return;
                    }
                }
            }
        }

        // If no valid buttons were hit, exit the current button state
        currentButton?.PlaybackCursorExited();
        currentButton = null;
    }
}
