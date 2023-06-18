using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class ButtonController : IDisposable
{
    ButtonView _buttonView;
    Popup _popup;
    IDisposable tooltipDelayDisposable;
    Coroutine tooltipCoroutine;
    private Recorder _recorder;
    private bool isDragging = false;
    private float clickThreshold = 0.2f;
    private EventTrigger trigger;
    private float pointerDownTime;

    public ButtonController(ButtonView buttonView, Popup popup, Recorder recorder)
    {
        _popup = popup;
        _buttonView = buttonView;
        _recorder = recorder;

        GameEvents.OnLeftClickDown += OnLeftClickDown;
        GameEvents.OnLeftClickHold += OnLeftClickHold;
        GameEvents.OnLeftClickUp += OnLeftClickUp;
        GameEvents.OnRightClickDown += OnRightClickDown;
        GameEvents.OnRightClickUp += OnRightClickUp;

        SetButtonBehaviors();
    }

    private void SetButtonBehaviors()
    {
        // Hovering
        _buttonView.myButton.OnPointerEnterAsObservable()
            .Subscribe(_ => OnPointerEnter());

        _buttonView.myButton.OnPointerExitAsObservable()
            .Subscribe(_ => OnPointerExit());

    }


    private void OnLeftClickDown()
    {
        if (_buttonView.pointerOnButton)
        {
            isDragging = true;
            pointerDownTime = Time.time;  
        }
    }

    private void OnLeftClickHold()
    {
        if (_buttonView.pointerOnButton)
        {
            _buttonView.StartCoroutine(LeftClickDownCoroutine());        
        }
    }


    private void OnLeftClickUp()
    {
        isDragging = false;
        if (_buttonView.pointerOnButton)
        {

            if(Time.time - pointerDownTime <= clickThreshold)
                {
                    OpenPupup();
                }
        }  
    }

    
    public void OnRightClickDown()
    {
        //ChangeColor();
    }

    public void OnRightClickUp()
    {
        if (_buttonView.pointerOnButton)
        {
            ChangeColor();
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

    private IEnumerator LeftClickDownCoroutine()
    {
        yield return new WaitForSeconds(clickThreshold);
        if(isDragging)
        {   
            OnDrag();
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


    private void OnDrag()
    {
            isDragging = true;
            Vector3 currentPosition = Input.mousePosition; 
            _buttonView.MyTransform.position = currentPosition;
    }

    public void OpenPupup()
    {
        if (_buttonView.pointerOnButton)
        {
            _popup.OpenPopup(_buttonView.buttonNumber);
        }
    }

    private void ChangeColor()
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

    public void Dispose()
    {
        tooltipDelayDisposable?.Dispose();
        tooltipDelayDisposable = null;

        if (tooltipCoroutine != null)
        {
            _buttonView.StopCoroutine(tooltipCoroutine);
            tooltipCoroutine = null;
        }

        GameEvents.OnLeftClickDown -= OnLeftClickDown;
        GameEvents.OnLeftClickDown -= OnLeftClickHold;
        GameEvents.OnLeftClickUp -= OnLeftClickUp;
        GameEvents.OnRightClickDown -= OnRightClickDown;
        GameEvents.OnRightClickUp -= OnRightClickUp;

        
    }
    

    public class Factory : PlaceholderFactory<ButtonView, ButtonController>
    {
    }
}
