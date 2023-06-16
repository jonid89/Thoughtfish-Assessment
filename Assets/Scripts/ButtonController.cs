using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UniRx.Triggers;

public class ButtonController : IDisposable
{
    ButtonView _buttonView;
    Popup _popup;
    IDisposable tooltipDelayDisposable;
    Coroutine tooltipCoroutine;
    private Recorder _recorder;
    private bool isDragging = false;
    private Vector2 dragOffset;
    private float clickThreshold = 0.1f;
    private float pointerDownTime;

    public ButtonController(ButtonView buttonView, Popup popup, Recorder recorder)
    {
        _popup = popup;
        _buttonView = buttonView;
        _recorder = recorder;
        SetButtonBehaviors();
    }

    private void SetButtonBehaviors()
    {
        // Left-Click
        _buttonView.myButton.OnPointerUpAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Where(_ => !isDragging)
            .Where(_ => (Time.time - pointerDownTime) < clickThreshold)
            .Subscribe(_ => ButtonClick());

        // Right-Click
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(1))
            .Where(_ => _buttonView.pointerOnButton)
            .Subscribe(_ => ChangeColor());

        // Hovering
        _buttonView.myButton.OnPointerEnterAsObservable()
            .Subscribe(_ => OnPointerEnter());

        _buttonView.myButton.OnPointerExitAsObservable()
            .Subscribe(_ => OnPointerExit());

        // Dragging
        var trigger = _buttonView.myButton.gameObject.AddComponent<EventTrigger>();

        // Add PointerDown event
        var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((data) => OnPointerDown((PointerEventData)data));
        trigger.triggers.Add(pointerDownEntry);

        // Add Drag event
        var dragEntry = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
        dragEntry.callback.AddListener((data) => OnDrag((PointerEventData)data));
        trigger.triggers.Add(dragEntry);

        // Add EndDrag event
        var endDragEntry = new EventTrigger.Entry { eventID = EventTriggerType.EndDrag };
        endDragEntry.callback.AddListener((data) => OnEndDrag((PointerEventData)data));
        trigger.triggers.Add(endDragEntry);

        // Add PointerUp event
        var pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((data) => OnPointerUp((PointerEventData)data));
        trigger.triggers.Add(pointerUpEntry);
    }

    public void ButtonClick()
    {
        _popup.OpenPopup(_buttonView.buttonNumber);

        if (_recorder != null && _recorder.IsRecording)
        {
            _recorder.RecordAction(); // Record the button click action
        }
    }

    public void ChangeColor()
    {
        if (_buttonView.myColor == Color.red)
        {
            _buttonView.SetColor(Color.green);
            return;
        }
        if (_buttonView.myColor == Color.green)
        {
            _buttonView.SetColor(Color.blue);
            return;
        }
        if (_buttonView.myColor == Color.blue)
        {
            _buttonView.SetColor(Color.red);
            return;
        }
    }

    private void OnPointerEnter()
    {
        tooltipDelayDisposable = Observable.Timer(TimeSpan.FromSeconds(_buttonView.tooltipDelay))
            .Subscribe(_ =>
            {
                if (_buttonView.pointerOnButton)
                {
                    tooltipCoroutine = _buttonView.StartCoroutine(ActivateTooltipCoroutine());
                }
            });
    }

    private void OnPointerExit()
    {
        _buttonView.tooltip.SetActive(false);
        tooltipDelayDisposable?.Dispose();
        tooltipDelayDisposable = null;

        if (tooltipCoroutine != null)
        {
            _buttonView.StopCoroutine(tooltipCoroutine);
            tooltipCoroutine = null;
        }
    }

    private IEnumerator ActivateTooltipCoroutine()
    {
        yield return new WaitForSeconds(_buttonView.tooltipDelay);

        if (_buttonView.pointerOnButton)
        {
            _buttonView.tooltip.SetActive(true);
        }
    }

    private void OnPointerDown(PointerEventData eventData)
    {
        if (_buttonView.pointerOnButton)
        {
            pointerDownTime = Time.time;
            isDragging = false;
            dragOffset = _buttonView.MyTransform.position - (Vector3)eventData.position;
        }
    }

    private void OnDrag(PointerEventData eventData)
    {
        if (_buttonView.pointerOnButton)
        {
            isDragging = true;
            Vector3 currentPosition = eventData.position + (Vector2)dragOffset;
            _buttonView.MyTransform.position = currentPosition;
        }
    }

    private void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void Dispose()
    {
        tooltipDelayDisposable?.Dispose();
        tooltipDelayDisposable = null;

        if (tooltipCoroutine != null)
        {
            _buttonView.StopCoroutine(tooltipCoroutine);
            tooltipCoroutine = null;
        }
    }

    public class Factory : PlaceholderFactory<ButtonView, ButtonController>
    {
    }
}
