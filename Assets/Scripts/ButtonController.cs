using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ButtonController : IDisposable
{
    ButtonView _buttonView;
    Popup _popup;

    public ButtonController(ButtonView buttonView, Popup popup) {
        _popup = popup;
        _buttonView = buttonView;
        _buttonView.myButton.OnClickAsObservable()
            .Subscribe(_ => ButtonClick());
    }

    public void ButtonClick(){
        _popup.OpenPopup(_buttonView.buttonNumber);
    }

    public void Dispose(){

    }

    public class Factory : PlaceholderFactory<ButtonView, Popup, ButtonController>
    {
    }
}
