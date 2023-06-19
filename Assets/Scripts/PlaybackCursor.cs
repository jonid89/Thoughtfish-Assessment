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
    private Button popupButton;
    private RectTransform rectTransform;
    private IDisposable updateDisposable;
    private bool leftClickDownTriggered = false;
    private bool isPopupLayer = false;


#region Singleton
    public static PlaybackCursor Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        
    }
#endregion

    public void SetActiveMethod()
    {
        this.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        updateDisposable = this.UpdateAsObservable()
            .Subscribe(_ => DetectButtonUnderCursor());

        GameEvents.OnLeftClickDown += HandleLeftClickDown;
    }

    private void OnDisable()
    {
        updateDisposable?.Dispose();

        GameEvents.OnLeftClickDown -= HandleLeftClickDown;
    }

    public void MoveCursor(Vector2 position)
    {
        rectTransform.position = position;
    }

    private void HandleLeftClickDown()
    {
        leftClickDownTriggered = true;
    }

    private void DetectButtonUnderCursor()
    {
        Vector2 rayOrigin = rectTransform.position;
        Vector2 rayDirection = Vector2.up; 
        float rayDistance = 1f; 

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, rayDirection, rayDistance);

        bool popupButtonHit = false;

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];

            if (IsLayerInMask(hit.collider.gameObject.layer, popupLayer))
            {
                isPopupLayer = true;
                popupButton = hit.collider.gameObject.GetComponent<Button>();

                if (popupButton != null)
                {
                    popupButtonHit = true;
                    break;
                }
            }
        }

        if (popupButtonHit && leftClickDownTriggered)
        {
            popupButton.onClick.Invoke();
        }
        else
        {
            if (!popupButtonHit && !isPopupLayer)
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

        leftClickDownTriggered = false;
        isPopupLayer = false;
    }

    private bool IsLayerInMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
