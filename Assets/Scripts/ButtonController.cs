using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ButtonController : IDisposable
{
    ButtonView _buttonView;

    public ButtonController(ButtonView buttonView) {
        _buttonView = buttonView;
        _buttonView.myButton.OnClickAsObservable()
            .Subscribe(_ => ButtonClick());
    }

    public void ButtonClick(){
        Debug.Log("Button Left-Clicked");
    }

    public void Dispose(){

    }

    public class Factory : PlaceholderFactory<ButtonView, ButtonController>
    {
    }
}
