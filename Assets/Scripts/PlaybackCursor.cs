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
        Vector2 rayDirection = Vector2.up;
        float rayDistance = 1f; 

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, rayDirection, rayDistance);

        // Check for Popup buttons first
        bool popupButtonHit = false;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (IsLayerInMask(hit.collider.gameObject.layer, popupLayer))
            {
                Button popupButton = hit.collider.gameObject.GetComponent<Button>();
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
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (IsLayerInMask(hit.collider.gameObject.layer, buttonLayer))
                {
                    ButtonView buttonView = hit.collider.GetComponent<ButtonView>();
                    buttonView.PlaybackCursorPosition(this.gameObject.transform.position);
                    if (buttonView != null)
                    {
                        if (currentButton != buttonView)
                        {
                            currentButton?.PlaybackCursorExited();
                            currentButton = buttonView;
                            currentButton.PlaybackCursorEntered();
                        }
                        return;
                    }
                }
            }
        }

        currentButton?.PlaybackCursorExited();
        currentButton = null;
    }

    private bool IsLayerInMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
