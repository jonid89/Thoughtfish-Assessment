using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ObjectSpawner : IDisposable, IInitializable
{
    ButtonView.Factory _buttonViewFactory;
    ButtonController.Factory _buttonControllerFactory;
    ButtonsSettings _buttonsSettings;
    Popup _popup;

    public ObjectSpawner(ButtonView.Factory buttonViewFactory, ButtonController.Factory buttonControllerFactory, ButtonsSettings buttonsSettings, Popup popup){
        _buttonViewFactory = buttonViewFactory;
        _buttonControllerFactory = buttonControllerFactory;
        _buttonsSettings = buttonsSettings;
        _popup = popup;
    }

    public void Initialize(){
        List<ButtonsSettings.MyButton> buttonsList = _buttonsSettings.myButtons;
        for(int i=0; i < buttonsList.Count; i++){
            //Instantiate Button
            var buttonView = _buttonViewFactory.Create();
            _buttonControllerFactory.Create(buttonView);
            //Set Button Config
            Vector2 position = new Vector2(-250+250*(i%buttonsList.Count),-50+50*(i/buttonsList.Count));
            buttonView.SetPosition(position);
            var buttonType = buttonsList[i];
            buttonView.SetColor(_buttonsSettings.colorConfig[buttonType.buttonColor]);
            buttonView.SetSprite(buttonType.buttonImage);
            buttonView.SetNumber(i+1);
            
        }
    }


    public void Dispose(){

    }


}
