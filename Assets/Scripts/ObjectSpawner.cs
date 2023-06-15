using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ObjectSpawner : IDisposable, IInitializable
{
    [SerializeField] public List<ButtonsSettings.MyButton> buttonsList = new List<ButtonsSettings.MyButton>();
    ButtonView.Factory _buttonViewFactory;
    ButtonController.Factory _buttonControllerFactory;
    ButtonsSettings _buttonsSettings;

    public ObjectSpawner(ButtonView.Factory buttonViewFactory, ButtonController.Factory buttonControllerFactory, ButtonsSettings buttonsSettings){
        _buttonViewFactory = buttonViewFactory;
        _buttonControllerFactory = buttonControllerFactory;
        _buttonsSettings = buttonsSettings;
    }

    public void Initialize(){
        //Debug.Log(buttonsList[0]);
        //Debug.Log(_buttonsSettings.myButtons[0]);
        buttonsList = _buttonsSettings.myButtons;
        for(int i=0; i < buttonsList.Count; i++){
            var buttonView = _buttonViewFactory.Create();
            Vector2 position = new Vector2(-250+250*(i%3),-50+50*(i/3));
            var buttonType = buttonsList[i];
            Color buttonColor = _buttonsSettings.colorConfig[buttonType.buttonColor];
            buttonView.SetColor(buttonColor);

            buttonView.SetPosition(position);
            buttonView.SetNumber(i+1);
            _buttonControllerFactory.Create(buttonView);
            //buttonNumberViewList.Add(buttonNumberView);
        }
    }


    public void Dispose(){

    }


}
