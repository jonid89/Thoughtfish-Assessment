using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class ButtonController : IDisposable
{
    ButtonView _buttonView;
    Popup _popup;

    public ButtonController(ButtonView buttonView, Popup popup) {
        _popup = popup;
        _buttonView = buttonView;
        SetButtonBehaviors();
    }


    private void SetButtonBehaviors(){
        //Left-Click
        _buttonView.myButton.OnClickAsObservable()
            .Subscribe(_ => ButtonClick());
        //Right-Click
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(1))
            .Subscribe(_ => {if(_buttonView.pointerOnButton) ChangeColor();} );
        //Hovering
        
    }

    public void ButtonClick(){
        _popup.OpenPopup(_buttonView.buttonNumber);
    }

     public void ChangeColor(){
        Debug.Log("changing color");
            if(_buttonView.myColor == Color.red) {_buttonView.SetColor(Color.green); return;}
            if(_buttonView.myColor == Color.green) {_buttonView.SetColor(Color.blue); return;}
            if(_buttonView.myColor == Color.blue) {_buttonView.SetColor(Color.red);return;}
     }

    public void Dispose(){

    }

    public class Factory : PlaceholderFactory<ButtonView, ButtonController>
    {
    }
}
