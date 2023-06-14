using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ObjectSpawner : IDisposable, IObjectConstructed
{
    [SerializeField] private List<GameObject> buttonsList = new List<GameObject>();

    //private List<ButtonNumberView> buttonNumberViewList = new List<ButtonNumberView>();

    ButtonNumberView.Factory _buttonNumberViewFactory;
    ButtonNumberController.Factory _buttonNumberControllerFactory;


    public ObjectSpawner(ButtonNumberView.Factory buttonNumberViewFactory, ButtonNumberController.Factory buttonNumberControllerFactory, ButtonsConfig buttonsConfig){
        _buttonNumberViewFactory = buttonNumberViewFactory;
        _buttonNumberControllerFactory = buttonNumberControllerFactory;
        OnObjectConstructed();
    }

    public void OnObjectConstructed(){
        Debug.Log("ObjectSpawner OnObject Constructed called");
        for(int i=0; i < buttonsList.Count; i++){
            var buttonNumberView = _buttonNumberViewFactory.Create();
            Vector2 position = new Vector2(-250+250*(i%3),-50+50*(i/3));
            buttonNumberView.SetPosition(position);
            buttonNumberView.SetNumber(i+1);
            _buttonNumberControllerFactory.Create(buttonNumberView);
            //buttonNumberViewList.Add(buttonNumberView);
        }
    }


    public void Dispose(){

    }


}
