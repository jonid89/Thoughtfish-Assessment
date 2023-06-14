using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ButtonNumberController : IDisposable
{
    ButtonNumberView _buttonNumberView;

    private int buttonNumber = 0;

    public ButtonNumberController(ButtonNumberView buttonNumberView) {
        _buttonNumberView = buttonNumberView;
        _buttonNumberView.myButton.OnClickAsObservable()
            .Subscribe(_ => ButtonClick());
    }

    public void ButtonClick(){
        Debug.Log("Button Left-Clicked");
    }

    public void Dispose(){

    }

    public class Factory : PlaceholderFactory<ButtonNumberView, ButtonNumberController>
    {
    }
}
